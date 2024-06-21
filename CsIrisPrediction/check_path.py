import sys

def check_sys_path():
    print("Python sys.path:")
    for path in sys.path:
        print(f"  {path}")

