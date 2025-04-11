@echo off
setlocal

REM === Step 1: Publish the .NET project ===
echo Publishing project...
dotnet publish -c Release -o ./publish
if errorlevel 1 (
    echo Failed to publish the project.
    exit /b 1
)

REM === Step 2: Upload to EC2 using SCP ===
echo Uploading files to EC2...
scp -i "..\my-ec2-keypair.pem" -r ./publish ubuntu@ec2-18-134-207-5.eu-west-2.compute.amazonaws.com:/home/ubuntu/
if errorlevel 1 (
    echo Failed to upload files via SCP.
    exit /b 1
)

echo Publish to EC2 complete.
