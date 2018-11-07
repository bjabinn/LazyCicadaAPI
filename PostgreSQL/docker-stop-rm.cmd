@echo off
setlocal enabledelayedexpansion
set id=
for %%A in (%*) do (
	docker ps -aqf "name=%%A" > "d-rm.tmp"
	set /p id=<d-rm.tmp
	docker stop !id!
	docker rm !id! --force
	del "d-rm.tmp" /q
)
docker ps -a
