using System;
using Python.Runtime;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Usage: IrisPredictionClient <sepal_length> <sepal_width> <petal_length> <petal_width>");
            return;
        }

        // Initialize the Python runtime
        PythonEngine.Initialize();

        // Define the scope
        using (Py.GIL())
        {
            try
            {
                // Import the predict script
                dynamic predict = Py.Import("predict");

                // Convert arguments to the appropriate types
                double sepal_length = Convert.ToDouble(args[0]);
                double sepal_width = Convert.ToDouble(args[1]);
                double petal_length = Convert.ToDouble(args[2]);
                double petal_width = Convert.ToDouble(args[3]);

                // Call the Python predict function
                var result = predict.predict(sepal_length, sepal_width, petal_length, petal_width);

                // Print the result
                Console.WriteLine($"Predicted species: {result}");
            }
            catch (PythonException ex)
            {
                Console.WriteLine($"Python error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Shutdown the Python runtime
        PythonEngine.Shutdown();
    }
}

