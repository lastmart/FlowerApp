# переедем на этот стораж, как только с Глебом договоримся о деплое, а с Егором - о контракте моделей
from typing import Generator
from models.flower_feature import FlowerFeature
import psycopg2

class FlowerFeaturesStorage:
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
    
    def get_flowers_count(self) -> int:
        self.__cursor.execute(
'''
SELECT COUNT(flower_id)
FROM flower_features
''')
        return self.__cursor.fetchone()[0]
    
    def get_unique_flowers(self) -> int:
        self.__cursor.execute(
'''
SELECT COUNT(DISTINCT flower_id)
FROM flower_features
''')
        return self.__cursor.fetchone()[0]
    
    def get_flowers_by_question_id(self, question_id: int) -> list:
        self.__cursor.execute(
f'''
SELECT *
FROM flower_features
WHERE question_id={question_id}
''')
        return self.__cursor.fetchall()   
    
    def iterate_flower_features_by_row_with_question_id(self, question_id: int) -> Generator[FlowerFeature, None, None]:
        for flower_row in self.get_flowers_by_question_id(question_id):
            yield FlowerFeature(
                int(flower_row['flower_id']),
                int(flower_row['question_id']),
                int(flower_row['features_size']),
                int(flower_row['feature'])
            )
