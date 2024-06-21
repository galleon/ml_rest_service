import py4j.GatewayServer;
import py4j.Py4JNetworkException;
import py4j.java_gateway.JavaGateway;

public class RunPythonWithPy4J {
    public static void main(String[] args) {
        if (args.length != 4) {
            System.out.println("Usage: IrisPredictionClient <sepal_length> <sepal_width> <petal_length> <petal_width>");
            return;
        }

        try {
            // Connect to the Python Gateway Server
            JavaGateway gateway = new JavaGateway();
            
            // Get the entry point (the PredictionService instance)
            Object predictionService = gateway.getEntryPoint();

            // Parse arguments
            double sepal_length = Double.parseDouble(args[0]);
            double sepal_width = Double.parseDouble(args[1]);
            double petal_length = Double.parseDouble(args[2]);
            double petal_width = Double.parseDouble(args[3]);

            // Call the predict method
            String result = (String) gateway.invoke(predictionService, "predict", sepal_length, sepal_width, petal_length, petal_width);
            System.out.println("Predicted species: " + result);

            // Close the gateway connection
            gateway.close();
        } catch (Py4JNetworkException e) {
            System.err.println("Could not connect to the Python Gateway Server. Ensure it is running.");
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}

