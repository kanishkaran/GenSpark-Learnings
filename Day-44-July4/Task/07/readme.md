

## Changing Image of the nginx service 

    kanishkaran@FVFG90G8Q05F- 6 % docker service ls
    ID             NAME                MODE         REPLICAS   IMAGE          PORTS
    r1a53pq3spe9   nginx-stack_nginx   replicated   3/3        nginx:latest   *:3000->80/tcp


### Change image using service update command

    anishkaran@FVFG90G8Q05F- 6 % docker service update --image nginx:latest nginx-stack_nginx
    nginx-stack_nginx
    overall progress: 3 out of 3 tasks 
    1/3: running   [==================================================>] 
    2/3: running   [==================================================>] 
    3/3: running   [==================================================>] 
    verify: Service nginx-stack_nginx converged 

### View change

    kanishkaran@FVFG90G8Q05F- 6 % docker service ps nginx-stack_nginx
    ID             NAME                      IMAGE          NODE             DESIRED STATE   CURRENT STATE             ERROR     PORTS
    i0z3a4xbyt1z   nginx-stack_nginx.1       nginx:latest   docker-desktop   Running         Running 39 seconds ago              
    luw0xlfbjfgn    \_ nginx-stack_nginx.1   web:latest     docker-desktop   Shutdown        Shutdown 50 seconds ago             
    ebdvxbja1z33   nginx-stack_nginx.2       nginx:latest   docker-desktop   Running         Running 35 seconds ago              
    r4cbetcxtu79    \_ nginx-stack_nginx.2   web:latest     docker-desktop   Shutdown        Shutdown 36 seconds ago             
    rimraum8o0qf   nginx-stack_nginx.3       nginx:latest   docker-desktop   Running         Running 32 seconds ago              
    y7xxdonh7yft    \_ nginx-stack_nginx.3   web:latest     docker-desktop   Shutdown        Shutdown 32 seconds ago             
    kanishkaran@FVFG90G8Q05F- 6 % 