



### TO PULL AND RUN A MYSQL IMAGE WITH PASSWORD AND ALLOCATION A VOLUME TO IT

```
docker run --name mysql-task \
  -e MYSQL_ROOT_PASSWORD=my-secret-pw \
  -v mydbdata:/var/lib/mysql \
  -p 3306:3306 \
  -d mysql:latest
```

### EXECUTING MYSQL THRU DOCKER CLI

```
docker exec -it mysql-task  mysql -u root -p
```

This opens up the mysql command line interface when we provide the password.


```sql

Enter password: 
Welcome to the MySQL monitor.  Commands end with ; or \g.
Your MySQL connection id is 9
Server version: 9.3.0 MySQL Community Server - GPL

Copyright (c) 2000, 2025, Oracle and/or its affiliates.

Oracle is a registered trademark of Oracle Corporation and/or its
affiliates. Other names may be trademarks of their respective
owners.

Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

mysql> CREATE DATABASE myDB;
Query OK, 1 row affected (0.014 sec)

mysql> USE myDB;
Database changed
mysql> CREATE TABLE usertable;
ERROR 4028 (HY000): A table must have at least one visible column.
mysql> CREATE TABLE usertable(id int, name varchar(20));
Query OK, 0 rows affected (0.016 sec)

mysql> INSERT INTO usertable VALUES(1, kanish),(2, karan);
ERROR 1054 (42S22): Unknown column 'kanish' in 'field list'
mysql> INSERT INTO usertable VALUES(1, 'kanish'),(2, 'karan');
Query OK, 2 rows affected (0.005 sec)
Records: 2  Duplicates: 0  Warnings: 0

mysql> SELECT * FROM usertable;
+------+--------+
| id   | name   |
+------+--------+
|    1 | kanish |
|    2 | karan  |
+------+--------+
2 rows in set (0.001 sec)
```

### STOPPING AND PRUNING THE CONTAINER


```
kanishkaran@FVFG90G8Q05F- 2 % docker stop mysql-task
mysql-task


kanishkaran@FVFG90G8Q05F- 2 % docker container prune           
WARNING! This will remove all stopped containers.
Are you sure you want to continue? [y/N] y

```


### RUNNING THE IMAGE WITH SAME VOLUME


```
kanishkaran@FVFG90G8Q05F- 2 % docker run --name mysql-task \              
  -e MYSQL_ROOT_PASSWORD=root \
  -v mysqldata:/var/lib/mysql \
  -p 3306:3306 \
  -d mysql:latest
```

Checking if data persist


```sql
kanishkaran@FVFG90G8Q05F- 2 % docker exec -it mysql-task  mysql -u root -p

Enter password: 
Welcome to the MySQL monitor.  Commands end with ; or \g.
Your MySQL connection id is 9
Server version: 9.3.0 MySQL Community Server - GPL

Copyright (c) 2000, 2025, Oracle and/or its affiliates.

Oracle is a registered trademark of Oracle Corporation and/or its
affiliates. Other names may be trademarks of their respective
owners.

Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

mysql> USE myDB;
Reading table information for completion of table and column names
You can turn off this feature to get a quicker startup with -A

Database changed
mysql> SELECT * FROM usertable;
+------+--------+
| id   | name   |
+------+--------+
|    1 | kanish |
|    2 | karan  |
+------+--------+
2 rows in set (0.001 sec)
```