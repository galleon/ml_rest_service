from py4j.java_gateway import GatewayServer
import predict  # Import your predict module

class PredictionService:
    def predict(self, sepal_length, sepal_width, petal_length, petal_width):
        return predict.predict(sepal_length, sepal_width, petal_length, petal_width)

if __name__ == "__main__":
    prediction_service = PredictionService()
    gateway = GatewayServer(port=25333, entry_point=prediction_service)
    gateway.start()
    print("Gateway Server Started")

