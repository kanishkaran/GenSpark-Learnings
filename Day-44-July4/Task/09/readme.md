

### Update with delay

        kanishkaran@FVFG90G8Q05F- 6 % docker service update --image httpd:alpine httpd 
    httpd
    overall progress: 2 out of 2 tasks 
    1/2: running   [==================================================>] 
    2/2: running   [==================================================>] 
    verify: Service httpd converged 
    kanishkaran@FVFG90G8Q05F- 6 % docker service ls 
    ID             NAME                MODE         REPLICAS   IMAGE          PORTS
    bl5hapa7r3br   httpd               replicated   2/2        httpd:alpine   
    r1a53pq3spe9   nginx-stack_nginx   replicated   0/5        nginx:latest   *:3000->80/tcp
    kanishkaran@FVFG90G8Q05F- 6 % docker service ps httpd
    ID             NAME          IMAGE          NODE             DESIRED STATE   CURRENT STATE             ERROR     PORTS
    es91wgwlc02x   httpd.1       httpd:alpine   docker-desktop   Running         Running 35 seconds ago              
    s16xk80959wz    \_ httpd.1   httpd:latest   docker-desktop   Shutdown        Shutdown 35 seconds ago             
    cwr2kjdjqszo   httpd.2       httpd:alpine   docker-desktop   Running         Running 47 seconds ago              
    ld45h5i3js1n    \_ httpd.2   httpd:latest   docker-desktop   Shutdown        Shutdown 54 seconds ago             
    kanishkaran@FVFG90G8Q05F- 6 % 