from dataclasses import dataclass


@dataclass
class RecomendationsResponse:
    Count: int
    FlowerIds: list[int]
