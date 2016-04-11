@echo off

for /d %%G in ("Hunter6\test\*") do dnx -p %%G test
rem dnx -p Hunter.Web.Test test