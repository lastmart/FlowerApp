from dataclasses import dataclass


@dataclass
class SurveyAnswer:
    question_mask: str
    question_id: int
    survey_id: int
