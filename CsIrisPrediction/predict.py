# predict.py

import joblib
import numpy as np

def load_model():
    knn_model = joblib.load('knn_model.pkl')
    scaler = joblib.load('scaler.pkl')
    return knn_model, scaler

def predict(sepal_length, sepal_width, petal_length, petal_width):
    knn_model, scaler = load_model()
    data = np.array([[sepal_length, sepal_width, petal_length, petal_width]])
    scaled_data = scaler.transform(data)
    prediction = knn_model.predict(scaled_data)
    target_names = ['setosa', 'versicolor', 'virginica']
    return target_names[prediction[0]]
