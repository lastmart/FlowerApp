# переедем на этот стораж, как только с Глебом договоримся о деплое, а с Егором - о контракте моделей
from typing import Generator
from models.internal_models.flower_feature import FlowerFeature
import psycopg2
import os


class FlowerFeaturesStorage:
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
    
    def get_flowers_count(self) -> int:
        self.__cursor.execute(
'''
SELECT COUNT("FlowerId")
FROM "SurveyFlowers"
''')
        return self.__cursor.fetchone()[0]
    
    def get_unique_flowers(self) -> int:
        self.__cursor.execute(
'''
SELECT DISTINCT "FlowerId"
FROM "SurveyFlowers"
''')
        return [element[0] for element in self.__cursor.fetchall()]
    
    def get_flowers_by_question_id(self, question_id: int) -> list:
        self.__cursor.execute(
f'''
SELECT "FlowerId", "SurveyQuestionId", "Variants", "RelevantVariantsProbabilities"
FROM "SurveyFlowers"
JOIN "SurveyQuestions" ON "SurveyQuestionId" = "SurveyQuestions"."Id"
WHERE "SurveyQuestionId"={question_id}
''')
        return self.__cursor.fetchall()   
    
    def iterate_flower_features_by_row_with_question_id(self, question_id: int) -> Generator[FlowerFeature, None, None]:
        for flower_row in self.get_flowers_by_question_id(question_id):
            yield self._parse_to_feature_flower(
                flower_row[0],
                flower_row[1],
                flower_row[2],
                flower_row[3]
            )

    def _parse_to_feature_flower(self, flower_id: str, question_id: str, variants: str, relevant_variants: str):
        relevant_variants = relevant_variants.split(';')
        feature = [float(variant) for variant in relevant_variants]
        return FlowerFeature(
            int(flower_id),
            int(question_id),
            len(variants.split(';')),
            feature
        )