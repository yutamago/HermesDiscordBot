# Hermes (Discord Bot)
This bot delivers messages for you to any other channel you name, however it will not tell them who wrote it.
You can either include the channel name where you sent the message from, or you can go fully anonymous.

### [Invite](https://discord.com/api/oauth2/authorize?client_id=825365730173517824&permissions=8&scope=bot%20applications.commands)

###Commands
- `!send #channel Message...` Sends a message to the given channel. Prepends the sender channel name to the message.
- `!secret #channel Secret Message...` Sends a message to the given channel. Omits the sender channel name.
- `!help` Lists all commands

## (Optional) Install on your own server
1. Compile
2. Enter generated Discord Bot Token into `HermesBot.runtimeconfig.json`
3. (Windows) Run `HermesBot.exe` / (Linux) Run `HermesBot` 

### Install as service on Linux
1. Copy your compiled files to `/root/HermesBot/`
2. copy `HermesBot.service` to `/etc/systemd/system/`
3. Enable auto-run on boot `systemctl enable HermesBot`
4. Run the service `systemctl start HermesBot`
