from dataclasses import dataclass


@dataclass
class SurveyFlower:
    relevants_variants_probablities: str
    flower_id: int
    question_id: int