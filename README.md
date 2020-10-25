# ApexScheduler
Small service enabling schduling of Netune Apex Trident. 

### Service installation
Copy files to from https://github.com/ibondy/ApexLoader/releases/tag/1.0 to the local folder. (i.e. c:\apexloader) Modify settings.json with your Apex values

Adjust configuration file with your values

Open Windows Terminal as Administrator and run:
**sc create "ApexScheduler Service" binPath="\apexscheduler.exe" start=auto DisplyName="Schedule tests on Neptune Apex Trident device"**

**sc start "ApexScheduler Service"**

### Clean up
Open Windows Terminal as Administrator and run:

**sc stop "ApexScheduler Service"**

**sc delete "ApexScheduler Service"**

Delete your local folder with app files.
