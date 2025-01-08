from dataclasses import dataclass
from survey_answer import SurveyAnswer


@dataclass
class Survey:
    user_id: int
    answers: list[SurveyAnswer]