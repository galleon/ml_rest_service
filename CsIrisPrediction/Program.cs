using System;
using Python.Runtime;

class Program {
    static void Main(string[] args) {
        if (args.Length != 4) {
            Console.WriteLine("Usage: IrisPredictionClient <sepal_length> <sepal_width> <petal_length> <petal_width>");
            return;
        }

        // Path to the global Python shared library
        string pyPath = @"/Users/alleon_g/.pyenv/versions/3.11.8/lib/libpython3.11.dylib";
        if (!System.IO.File.Exists(pyPath)) {
            Console.WriteLine($"Python library not found at path: {pyPath}");
            return;
        }

        // Set the Python DLL
        Runtime.PythonDLL = pyPath;

        // Set the virtual environment's site-packages path
        string venvPath = @"/Users/alleon_g/.pyenv/versions/training";
        string sitePackagesPath = $"{venvPath}/lib/python3.11/site-packages";

        // Ensure the environment variables do not interfere
        Environment.SetEnvironmentVariable("PYTHONHOME", null);
        Environment.SetEnvironmentVariable("PYTHONPATH", sitePackagesPath);

        try {
            // Initialize the Python runtime
            PythonEngine.Initialize();

            using (Py.GIL()) {
                dynamic sys = Py.Import("sys");

                // Ensure the virtual environment's site-packages is in the Python path
                sys.path.append(sitePackagesPath);

                // Ensure the directory containing predict.py is in the Python path
                sys.path.append("./");

                // Check sys.path
                dynamic check_path = Py.Import("check_path");
                check_path.check_sys_path();

                try {
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
                } catch (PythonException ex) {
                    Console.WriteLine($"Python error: {ex.Message}");
                } catch (Exception ex) {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        } catch (Exception ex) {
            Console.WriteLine($"Failed to initialize Python runtime: {ex.Message}");
        } finally {
            try {
                // Shutdown the Python runtime
                PythonEngine.Shutdown();
            } catch (Exception ex) {
                Console.WriteLine($"Failed to shutdown Python runtime: {ex.Message}");
            }
        }
    }
}
