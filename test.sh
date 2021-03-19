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

echo OK