from fastapi import FastAPI
from recsys.recsys import RecSys
from models.recomendations_response import RecomendationsResponse


app = FastAPI()
recsys = RecSys()

@app.get("/recsys/get/")
async def get_recomendations(user_id: int, take: int = 5) -> RecomendationsResponse:
    return recsys.get_recomendations_for_user(user_id, take)
