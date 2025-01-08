import pandas as pd

from typing import Generator
from models.internal_models.flower_feature import FlowerFeature
import os

CURRENT_DIRECTORY = os.path.dirname(os.path.abspath(__file__))

class FlowerFeaturesStorage:
    PATH_TO_FLOWERS = CURRENT_DIRECTORY + "/flower_profiles.csv"
    
    def __init__(self):
        self.__flowers_data = pd.read_csv(self.PATH_TO_FLOWERS)
    
    def get_flowers_count(self) -> int:
        return self.__flowers_data['flower_id'].nunique()
    
    def get_unique_flowers(self) -> int:
        return self.__flowers_data['flower_id'].unique()
    
    def get_flowers_by_question_id(self, question_id: int) -> pd.DataFrame:
        return self.__flowers_data[self.__flowers_data['question_id'] == question_id]
    
    def iterate_flower_features_by_row_with_question_id(self, question_id: int) -> Generator[FlowerFeature, None, None]:
        matching_data = self.get_flowers_by_question_id(question_id)
        for _, flower_row in matching_data.iterrows():
            yield FlowerFeature(
                int(flower_row['flower_id']),
                int(flower_row['question_id']),
                int(flower_row['features_size']),
                int(flower_row['feature'])
            )