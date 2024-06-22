# Running a Python ML model from Java

Assuming that:
- your Python version is located in `$HOME/.pyenv/3.11.8`
- your dependencies are in `$HOME/.pyenv/jep`
- your model and associated files are in your current directory

Then you will run the code using:

```bash
export PYTHONHOME=~/.pyenv/versions/3.11.8
export PYTHONPATH=$PYTHONHOME/lib/python3.11:$PYTHONHOME/lib/python3.11/site-packages:~/.pyenv/versions/jep/lib/python3.11/site-packages:.
javac -cp ~/.pyenv/versions/jep/lib/python3.11/site-packages/jep/jep-4.2.0.jar RunPythonWithJep.java
java -cp .:jep-4.2.0.jar -Djava.library.path=~/.pyenv/versions/jep/lib/python3.11/site-packages/jep RunPythonWithJep 5.1 3.5 1.4 0.2
```

You should get: `setosa`
