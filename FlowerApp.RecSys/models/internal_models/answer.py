from dataclasses import dataclass


@dataclass
class Answer:
    user_id: int
    question_id: int
    # size answers in multinanswering
    answers_size: int
    # answer - bit mask for multianswering
    answer: list[int] 