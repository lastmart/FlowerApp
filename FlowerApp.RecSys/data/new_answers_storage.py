# переедем на этот стораж, как только с Глебом договоримся о деплое, а с Егором - о контракте моделей
from typing import Generator
from models.internal_models.answer import Answer
import psycopg2
import os


class AnswersStorage:
    def __init__(self):
        default_connection_string = ""
        if os.path.exists("data/default_connection"):
            with open("data/default_connection", 'r') as file:
                default_connection_string = file.read()
        connection_string = os.getenv("DefaultConnection", default_connection_string)

        self.__conn = psycopg2.connect(connection_string)
        self.__cursor = self.__conn.cursor()
        self.__conn.autocommit = True

    def __enter__(self):
        return self

    def __exit__(self, exc_type, exc_val, exc_tb):
        self.__cursor.close()
        self.__conn.close()

    def get_users_count(self) -> int:
        self.__cursor.execute(
'''
SELECT COUNT("Id")
FROM "Surveys"
''')
        return self.__cursor.fetchone()[0]
    
    def get_unique_users(self) -> int:
        self.__cursor.execute(
'''
SELECT DISTINCT "UserId"
FROM "Surveys"
''')
        return [element[0] for element in self.__cursor.fetchall()]
    
    def get_questions_count(self) -> int:
        self.__cursor.execute(
'''
SELECT COUNT(DISTINCT "QuestionId")
FROM "SurveyAnswers"
''')
        return self.__cursor.fetchone()[0]

    def iterate_answers_by_row(self) -> Generator[Answer, None, None]:
        self.__cursor.execute(
'''
SELECT "UserId", "QuestionId", "QuestionsMask", "Variants"
FROM "Surveys"
JOIN "SurveyAnswers" ON "Surveys"."Id" = "SurveyAnswers"."SurveyId"
JOIN "SurveyQuestions" ON "SurveyAnswers"."QuestionId" = "SurveyQuestions"."Id"
''')
        for answer_row in self.__cursor.fetchall():
            yield self._parse_to_answer(
                answer_row[0],
                answer_row[1],
                answer_row[2],
                answer_row[3])

    def _parse_to_answer(self, user_id: str, question_id: str, questions_mask: str, variants: str):
        questions_mask = questions_mask.split(';')
        answer = [int(variant) for variant in questions_mask]
        return Answer(user_id,
                      question_id,
                      len(questions_mask),
                      answer)
