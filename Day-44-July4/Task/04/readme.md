

### Create Network


    kanishkaran@FVFG90G8Q05F- 4 % docker network create task-4-network
    b313dbb0e385a406df01c1fb8daedc4f1fef52047aadd8f73eb11bbc354ed9db

### Run Backend with name and network

    kanishkaran@FVFG90G8Q05F- 4 % docker run  -p 5000:5000 --name api --network task-4-network task-4-api
    server is listening at port: 5000


### Run the frontend

    kanishkaran@FVFG90G8Q05F- Day-44-July4 % docker run -d -p 3000:80 --name web --network task-4-network task-4-fe
    c8d01d60c693db1ccf66f7f0a53542c46f2221188b9148bba7c7d45b35e78b4d

### Inspect the network

    kanishkaran@FVFG90G8Q05F- Day-44-July4 % docker network inspect task-4-network                                 
    [
        {
            "Name": "task-4-network",
            "Id": "b313dbb0e385a406df01c1fb8daedc4f1fef52047aadd8f73eb11bbc354ed9db",
            "Created": "2025-07-06T13:16:54.602895632Z",
            "Scope": "local",
            "Driver": "bridge",
            "EnableIPv4": true,
            "EnableIPv6": false,
            "IPAM": {
                "Driver": "default",
                "Options": {},
                "Config": [
                    {
                        "Subnet": "172.20.0.0/16",
                        "Gateway": "172.20.0.1"
                    }
                ]
            },
            "Internal": false,
            "Attachable": false,
            "Ingress": false,
            "ConfigFrom": {
                "Network": ""
            },
            "ConfigOnly": false,
            "Containers": {
                "baed84d04f8cdf32564835ce6babe6cda3d131eab5bbe9c6507c9bd9e036f945": {
                    "Name": "api",
                    "EndpointID": "34400b907a50f59e43a29622dc575bbeb146717c6c5632f6e5c58098230fa9fa",
                    "MacAddress": "86:97:3a:9e:68:24",
                    "IPv4Address": "172.20.0.2/16",
                    "IPv6Address": ""
                },
                "c8d01d60c693db1ccf66f7f0a53542c46f2221188b9148bba7c7d45b35e78b4d": {
                    "Name": "web",
                    "EndpointID": "b43266f5e514e5ea65e92ed499c1192a2833ae1deee16c3b12e3f48fc546f7f6",
                    "MacAddress": "4a:b5:24:7f:57:bd",
                    "IPv4Address": "172.20.0.3/16",
                    "IPv6Address": ""
                }
            },
            "Options": {
                "com.docker.network.enable_ipv4": "true",
                "com.docker.network.enable_ipv6": "false"
            },
            "Labels": {}
        }
    ]


### Ping to backend via name


    kanishkaran@FVFG90G8Q05F- Day-44-July4 % docker exec -it web sh         
    / # ping api
    PING api (172.20.0.2): 56 data bytes
    64 bytes from 172.20.0.2: seq=0 ttl=64 time=0.197 ms
    64 bytes from 172.20.0.2: seq=1 ttl=64 time=0.089 ms
    64 bytes from 172.20.0.2: seq=2 ttl=64 time=0.272 ms
    64 bytes from 172.20.0.2: seq=3 ttl=64 time=0.267 ms
    64 bytes from 172.20.0.2: seq=4 ttl=64 time=0.303 ms
    ^C
    --- api ping statistics ---
    5 packets transmitted, 5 packets received, 0% packet loss
    round-trip min/avg/max = 0.089/0.225/0.303 ms
    / # exit