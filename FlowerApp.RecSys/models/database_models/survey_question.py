from dataclasses import dataclass


@dataclass
class SurveyQuestion:
    id: int
    variants: str