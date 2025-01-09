from dataclasses import dataclass


@dataclass
class FlowerFeature:
    flower_id: int
    question_id: int
    features_size: int
    feature: list[float]