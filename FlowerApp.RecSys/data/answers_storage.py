import pandas as pd

from typing import Generator
from models.internal_models.answer import Answer
import os

CURRENT_DIRECTORY = os.path.dirname(os.path.abspath(__file__))


class AnswersStorage:
    PATH_TO_ANSWERS = CURRENT_DIRECTORY + "/user_answers.csv"
    
    def __init__(self):
        self.__answers_data = pd.read_csv(self.PATH_TO_ANSWERS)

    def get_users_count(self) -> int:
        return self.__answers_data['user_id'].nunique()
    
    def get_unique_users(self) -> int:
        return self.__answers_data['user_id'].unique()
    
    def get_questions_count(self) -> int:
        return self.__answers_data['question_id'].nunique()

    def iterate_answers_by_row(self) -> Generator[Answer, None, None]:
        for _, answer_row in self.__answers_data.iterrows():
            yield Answer(
                int(answer_row['user_id']),
                int(answer_row['question_id']),
                int(answer_row['answers_size']),
                int(answer_row['answer'])
            )