#!/bin/bash
assert() {
  expected="$1"
  input="$2"

  ./bin/Debug/net5.0/linux-x64/publish/mplc "$input" > tmp.s
  # dotnet run "$input" > tmp.s
  cc -o tmp tmp.s
  ./tmp
  actual="$?"

  if [ "$actual" = "$expected" ]; then
    echo "$input => $actual"
  else
    echo "$input => $expected expected, but got $actual"
    exit 1
  fi
}

assert 0 "0;"
assert 42 "42;"
assert 8 "5 + 3;"
assert 10 "20 - 10;"
assert 6 "5 + 3 - 4 + 2;"
assert 9 "10 + 8 - 3 - 5 + 2 - 2 - 1;"
assert 10 "2 * 5;"
assert 10 "1 + 3 * 3;"
assert 20 "2 * 5 + 3 * 8 - 7 * 2;"
assert 5 "10 / 2;"
assert 7 "8 / 2 + 3;"
assert 10 "5 + (2 + 3);"
assert 12 "3 * (3 + 1);"
assert 20 "2 * (5 * (3 - 1));"
assert 2 "-2 + 4;"
assert 10 "-3 * -3 + 1;"
assert 5 "-10 / -2;"
assert 10 "-((3 + 2) * 4) / -(9 - 7);"
assert 1 "3 == 3;"
assert 0 "4 == 2;"
assert 1 "5 != 20;"
assert 1 "2 < 3;"
assert 0 "5 > 5;"
assert 1 "4 >= 4;"
assert 1 "3 <= 3;"
assert 1 "4 + 2 == 2 * 3;"
assert 3 "4; 3;"
assert 3 "a; a = 3; a;"
assert 4 "a; b; a = 3; b = 1; a + b;"
assert 3 "sum; sum = 0; sum = sum + 1; sum = sum + 2; sum;"
assert 9 "num1 = 3; num2 = 5; num1 = num1 * num2 + num2 - num1 * num2 + num1 + (num1 != num2); num1;"
assert 11 "return 11;"
assert 10 "value = 10; _ = 32; return value;"
assert 10 "n1 = 3; n2 = 4; return n2 * n2 - n1 - n1;"
assert 10 "n1 = 5; n2 = 10; if (n1 == 5) return n2; else return n1;"
assert 20 "n1 = 10; n2 = 20; n3 = 30; n4; if (n1 == 30) n4 = n1; else if (n1 == 20) n4 = n3; else if (n1 == 10) n4 = n2; else n4 = 5; return n4;"
assert 10 "n1 = 0; while (n1 != 10) n1 = n1 + 1; return n1;"
assert 20 "n1 = 0; while (1) if (n1 == 20) return n1; else n1 = n1 + 1;"

echo OK