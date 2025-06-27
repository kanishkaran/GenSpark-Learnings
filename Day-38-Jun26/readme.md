## Linux File Commands

### 1. You have a file with permissions -rw-r--r--, and you run chmod +x file.sh. What happens?
    - This adds execute or x permission to all users i.e user, groups and others
     -rw-r--r-- changes to -rwxr-xr-x
### 2. What is the difference between chmod 744 file.txt and chmod u=rwx,go=r file.txt?
    - The 744 means all permission to user, only read / r permission to group and same read permission to others `denoted by : -rwxr--r--`
    - Whereas the command chmod u=rwx,go=r file.txt gives the same permissions but different form 

### 3. What is the sticky bit, and when should you use it?
    a special file permission that, when set on a directory, restricts file deletion and renaming to only the file owner, directory owner, or the root user
    
    Example:
    The /tmp directory is often used as a temporary storage area and has the sticky bit set.
    This ensures that users can create and delete their own temporary files, but they cannot delete files created by other users, even if those files have write permissions for the directory. 

### 4. You are told to give the owner full access, group only execute, and others no permissions. What symbolic command achieves this?

    `Octal code 710`
    - if file: `-rwx--x---` 
    - if directory: `drwx--x---`


### 5. What is umask, and why is it important?
    umask is a command which helps in giving default permissions to new files and folders.

### 6. If the umask is 022, what are the default permissions for a new file and a new directory?
    with 022 umask the default permission for file would be -rw-r--r-- and folders would be drwx-r-xr-x

### 7. Why is umask often set to 002 in development environments but 027 or 077 in production?
    In a dev env users can have all permission and the group can also have all permissions but others can not have write permissions.
    In a prod env only the users should have all permissions so that others (here includes group and all others) dont change anything 
    
### 8. useradd vs adduser
    - useradd is a low level command which requires options to be provided and gives fine-grained control over addition of a user
    - adduser is high-level command which can be said as a wrapper around useradd. Its interactive, meaning it prompts some information or manual (kind of)