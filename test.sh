#!/bin/bash
assert() {
  expected="$1"
  input="$2"

  dotnet run "$input" > tmp.s
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

assert 0 0
assert 42 42
assert 8 "5 + 3"
assert 10 "20 - 10"
assert 6 "5 + 3 - 4 + 2"
assert 9 "10 + 8 - 3 - 5 + 2 - 2 - 1"

echo OK