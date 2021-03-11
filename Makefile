mplc:
	dotnet publish -c Debug -r linux-x64 --self-contained true

test: mplc
	./test.sh