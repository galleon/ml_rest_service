from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import joblib
import numpy as np

# Load the model and scaler
knn_model = joblib.load('knn_model.pkl')
scaler = joblib.load('scaler.pkl')

app = FastAPI()

# Define the input schema using Pydantic
class IrisRequest(BaseModel):
    sepal_length: float
    sepal_width: float
    petal_length: float
    petal_width: float

class IrisResponse(BaseModel):
    species: str

# Map the target names to the species names
target_names = ['setosa', 'versicolor', 'virginica']

@app.post('/predict', response_model=IrisResponse)
def predict(iris: IrisRequest):
    try:
        # Prepare the input data
        data = np.array([[iris.sepal_length, iris.sepal_width, iris.petal_length, iris.petal_width]])
        scaled_data = scaler.transform(data)
        
        # Perform the prediction
        prediction = knn_model.predict(scaled_data)
        species = target_names[prediction[0]]
        
        # Return the prediction as a response
        return IrisResponse(species=species)
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

# Define a root endpoint
@app.get('/')
def read_root():
    return {"message": "Welcome to the Iris Prediction API!"}

