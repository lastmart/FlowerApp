# переедем на этот стораж, как только с Глебом договоримся о деплое, а с Егором - о контракте моделей
from typing import Generator
from models.internal_models.answer import Answer
import psycopg2
import os


class AnswersStorage:
    def __init__(self):
        default_connection_string = ""
        if os.path.exists("./default_connection"):
            with open("./default_connection", 'r') as file:
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
SELECT COUNT(DISTINCT "UserId")
FROM "Surveys"
''')
        return self.__cursor.fetchall()
    
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
JOIN "Questions" ON "SurveyAnswers"."QuestionId" = "Questions"."Id"
''')
        for answer_row in self.__cursor.fetchall():
            yield self._parse_to_answer(
                answer_row['UserId'],
                answer_row['QuestionId'],
                answer_row['QuestionsMask'],
                answer_row['Variants'])

    def _parse_to_answer(self, user_id: str, question_id: str, questions_mask: str, variants: str):
        sorted_variants = sorted(variants.split(';'))
        questions_mask = questions_mask.split(';')
        answer = sum(1 << i for i, variant in enumerate(sorted_variants) if variant in questions_mask)
        return Answer(user_id,
                      question_id,
                      len(sorted_variants),
                      answer)
