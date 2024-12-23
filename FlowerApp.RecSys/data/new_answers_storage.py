# переедем на этот стораж, как только с Глебом договоримся о деплое, а с Егором - о контракте моделей
from typing import Generator
from models.answer import Answer
import psycopg2


class AnswersStorage:
    def __init__(self):
        self.__conn = psycopg2.connect("""
    host=
    port=
    sslmode=
    dbname=
    user=
    password=
    target_session_attrs=
""")
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
SELECT COUNT(id)
FROM users
''')
        return self.__cursor.fetchone()[0]
    
    def get_unique_users(self) -> int:
        self.__cursor.execute(
'''
SELECT DISTINCT id
FROM users
''')
        return self.__cursor.fetchall()
    
    def get_questions_count(self) -> int:
        self.__cursor.execute(
'''
SELECT COUNT(DISTINCT question_id)
FROM users
''')
        return self.__cursor.fetchone()[0]

    def iterate_answers_by_row(self) -> Generator[Answer, None, None]:
        self.__cursor.execute(
'''
SELECT *
FROM users
''')
        for answer_row in self.__cursor.fetchall():
            yield Answer(
                int(answer_row['user_id']),
                int(answer_row['question_id']),
                int(answer_row['answers_size']),
                int(answer_row['answer'])
            )
