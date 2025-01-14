import numpy as np
import pandas as pd

from scipy.sparse.linalg import svds
from sklearn.metrics.pairwise import cosine_similarity
from data.new_answers_storage import AnswersStorage
from data.new_flower_features_storage import FlowerFeaturesStorage
from models.recomendations_response import RecomendationsResponse


class RecSys:
    def __init__(self):
        self.__answers_storage = AnswersStorage()
        self.__flower_features_storage = FlowerFeaturesStorage()
        self.__flowers_size = self.__flower_features_storage.get_flowers_count()
        self.__questions_size = self.__answers_storage.get_questions_count()
        self.__count_latent_component = 10
        self.__cache = {}
    
    def get_recomendations_for_user(self, user_id: int, take: int) -> RecomendationsResponse:
        if user_id in self.__cache and\
           not self.__try_update_questions_size() and\
           not self.__try_update_flowers_size():
               response = self.__cache[user_id]
               return RecomendationsResponse(len(response), response)
        
        answer_matrix = np.zeros((self.__answers_storage.get_users_count(), self.__flowers_size))
        user_ids = []
        flower_ids = []
        for answer_row in self.__answers_storage.iterate_answers_by_row():
            for flower_feature_row in\
                self.__flower_features_storage.iterate_flower_features_by_row_with_question_id(
                    answer_row.question_id):
                answer_array = self.__decode_mask(
                    answer_row.answer,
                    flower_feature_row.features_size)
                flower_feature_array = self.__decode_mask(
                    flower_feature_row.feature,
                    flower_feature_row.features_size)
                
                if (answer_row.user_id not in user_ids):
                    user_ids.append(answer_row.user_id)
                if (flower_feature_row.flower_id not in flower_ids):
                    flower_ids.append(flower_feature_row.flower_id)
                match_score = np.dot(answer_array, flower_feature_array)
                answer_matrix[user_ids.index(answer_row.user_id) , flower_ids.index(flower_feature_row.flower_id)] = match_score

        self.__count_latent_component = min(self.__count_latent_component, answer_matrix.shape[1] - 1)
        return self.__find_k_best_flower_for_user(user_id, take, answer_matrix)

    def __decode_mask(self, decimal, num_bits):
        return np.array([int(bit) for bit in f"{decimal:0{num_bits}b}"])
    
    def __find_cosine_similarity(self, data, user_id):
        user_preferences = data[:, user_id]
        user_preferences = user_preferences.reshape(1, -1)
        flower_similarities = cosine_similarity(user_preferences, data.T).flatten()
        top_flower_indices = flower_similarities.argsort()[::-1]
        return top_flower_indices

    def __find_k_best_flower_for_user(self, user_id, k, answer_matrix):
        U, S, Vt = svds(answer_matrix, k=self.__count_latent_component)
        S = np.diag(S)
        
        approximated_matrix = np.dot(np.dot(U, S), Vt)
        user_index = list(self.__answers_storage.get_unique_users()).index(user_id)
        best_indexes = self.__find_cosine_similarity(approximated_matrix, user_index)[:k]
        flower_ids = self.__flower_features_storage.get_unique_flowers()
        return [flower_ids[i] for i in best_indexes]

    def __try_update_flowers_size(self):
        temp_size = self.__flower_features_storage.get_flowers_count()
        if self.__flowers_size == temp_size:
            return False
        
        self.__flowers_size = temp_size
        return True
    
    def __try_update_questions_size(self):
        temp_size = self.__answers_storage.get_questions_count()
        if self.__questions_size == temp_size:
            return False

        self.__questions_size = temp_size
        return True
