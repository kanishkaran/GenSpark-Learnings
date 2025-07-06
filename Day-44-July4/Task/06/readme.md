

### Create swarm and deploy stack

    kanishkaran@FVFG90G8Q05F- 6 % docker swarm init
    Swarm initialized: current node (doinse0t39i6375tax460bem7) is now a manager.


    kanishkaran@FVFG90G8Q05F- 6 % docker stack deploy -c compose.yml nginx-stack
    Since --detach=false was not specified, tasks will be created in the background.
    In a future release, --detach=false will become the default.
    Creating network nginx-stack_default
    Creating service nginx-stack_nginx


### View stacks and containers

    kanishkaran@FVFG90G8Q05F- 6 % docker service ls                             
    ID             NAME                MODE         REPLICAS   IMAGE        PORTS
    r1a53pq3spe9   nginx-stack_nginx   replicated   3/3        web:latest   *:3000->80/tcp



    kanishkaran@FVFG90G8Q05F- 6 % docker service ps nginx-stack_nginx
    ID             NAME                  IMAGE        NODE             DESIRED STATE   CURRENT STATE                ERROR     PORTS
    luw0xlfbjfgn   nginx-stack_nginx.1   web:latest   docker-desktop   Running         Running about a minute ago             
    r4cbetcxtu79   nginx-stack_nginx.2   web:latest   docker-desktop   Running         Running about a minute ago             
    y7xxdonh7yft   nginx-stack_nginx.3   web:latest   docker-desktop   Running         Running about a minute ago             


### CURL

```html
kanishkaran@FVFG90G8Q05F- 6 % curl localhost:3000
<!DOCTYPE html>
<html>
<head>
<title>Welcome to nginx!</title>
<style>
html { color-scheme: light dark; }
body { width: 35em; margin: 0 auto;
font-family: Tahoma, Verdana, Arial, sans-serif; }
</style>
</head>
<body>
<h1>Welcome to nginx!</h1>
<p>If you see this page, the nginx web server is successfully installed and
working. Further configuration is required.</p>

<p>For online documentation and support please refer to
<a href="http://nginx.org/">nginx.org</a>.<br/>
Commercial support is available at
<a href="http://nginx.com/">nginx.com</a>.</p>

<p><em>Thank you for using nginx.</em></p>
</body>
```