

## SWARM VISUALISER

### Running the viualiser in a swarm cluster

    kanishkaran@FVFG90G8Q05F- 6 % docker run -it -d -p 5000:8080 -v /var/run/docker.sock:/var/run/docker.sock dockersamples/visualizer  
    WARNING: The requested image's platform (linux/amd64) does not match the detected host platform (linux/arm64/v8) and no specific platform was requested
    1ac6596fde5ea33ad749fc82f308a3b9de304e15d71880146df85034ca580eeb

### Output

![image](./swarm-visualiser.png)