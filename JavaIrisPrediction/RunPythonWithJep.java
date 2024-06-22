import jep.Interpreter;
import jep.SharedInterpreter;

public class RunPythonWithJep {
    public static void main(String[] args) {
        if (args.length != 4) {
            System.out.println("Usage: IrisPredictionClient <sepal_length> <sepal_width> <petal_length> <petal_width>");
            return;
        }

        try (Interpreter interp = new SharedInterpreter()) {
            interp.exec("import predict");

            double sepal_length = Double.parseDouble(args[0]);
            double sepal_width = Double.parseDouble(args[1]);
            double petal_length = Double.parseDouble(args[2]);
            double petal_width = Double.parseDouble(args[3]);

            interp.set("sepal_length", sepal_length);
            interp.set("sepal_width", sepal_width);
            interp.set("petal_length", petal_length);
            interp.set("petal_width", petal_width);

            interp.exec("specie = predict.predict(sepal_length, sepal_width, petal_length, petal_width)");
            String specie = (String) interp.getValue("specie");

            System.out.println("Predicted species: " + specie);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}

