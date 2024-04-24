## How to run a service(api/database) locally and access it via another machine?

Let's suppose the service runs locally on `http://localhost:5000`. Here I am
using **PORT**: `5000` as example, you will have to replace it with your own
port.

- **Step 1** : If it's running on `localhost` convert the service to run on
  `0.0.0.0`. Why? Because `0.0.0.0` allows service to accept requests from other
  ip addresses and not only localhost. So, the url to run service on is now
  `http://0.0.0.0:5000`
- **Step 2** : There is another problem now, our windows runs firewall by
  default which blocks any incoming requests from other machines on our wifi
  network. So, we have to allow incoming request for `TCP` connections at port
  `5000`.

  - **Easy Way**: Just **disable firewall**.
    <p style="text-align: center;"><strong>OR</strong></p>
  - Open `Windows Defender Firewall With Advanced Security` > `Inbound Rules` >
    Under `Actions` Select `New rule` > Select `Port` and `Next`> Select `TCP`
    and Give `Specific ports` i.e 5000 and `Next` > Click `Next` until finish >
    Give `Name` > `Apply` and `OK`.
    <p style="text-align: center;"><strong>OR</strong></p>
  - **Recommended**: Run the below command in **Administrator Powershell**

    ```powershell
    New-NetFirewallRule -DisplayName "Allow Port 5000" -Direction Inbound -Protocol TCP -LocalPort 5000 -Action Allow
    ```

- **Step 3** : Skip this step if you are not running your service in WSL. As
  `wsl` is like a Virtual Machine over `Windows` it is isolated from the
  networking stuff of windows. So even if our windows machine receives request
  from another machine in our network, it still can't go to our service running
  inside the `wsl`. Get the WSL's ip address `ip addr`.
  ```sh
  ash@LAP-2099 ~> ip addr | grep eth0 | grep inet
      inet 172.31.141.1/20 brd 172.31.143.255 scope global eth0
  ash@LAP-2099 ~>
  ```
  Here WSL's ip is `172.31.141.1`. Now we want connection between `0.0.0.0` to
  `172.31.141.1`. To create this connection, Open administrator powershell and
  run
  ```powershell
  netsh interface portproxy add v4tov4 listenport=5000 listenaddress=0.0.0.0 connectport=5000 connectaddress=172.31.141.1
  ```
  To check if the connection is created run
  ```powershell
  netsh interface portproxy show v4tov4
  ```
- **Step 4** : Open `cmd` and run `ipconfig`. Find the first occurence of Ipv4
  address that is our ip address of our machine in the wifi network.

  ```
   Wireless LAN adapter Wi-Fi:

   Connection-specific DNS Suffix  . : GMG.LOCAL
   Link-local IPv6 Address . . . . . : fe80::c8e3:47b0:7ce5:7d7%15
   IPv4 Address. . . . . . . . . . . : 192.168.2.143
   Subnet Mask . . . . . . . . . . . : 255.255.248.0
   Default Gateway . . . . . . . . . : 192.168.2.1
  ```

- **Step 5** : Everything is done now. My **IP**: `192.168.2.143` and **PORT**:
  `5000`.

### Now service is available at `http://192.168.2.143:5000`.
