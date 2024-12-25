from dataclasses import dataclass


@dataclass
class Answer:
    user_id: int
    question_id: int
    # size answers in multinanswering
    answers_size: int
    # answer < 2 ** (answers_size + 1),
    # because answer - bit mask for multianswering
    answer: int 