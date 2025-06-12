
export class AsyncService{

    getUsersCb(callback : (user: any[]) => void): void{
        setTimeout(() => {
            const dummyUser = [{id: 1, name: "Alan"},
                {id: 2, name: "Mark"}
            ];

            callback(dummyUser);
        }, 1000);
    }

    getUsersPromise(): Promise<any[]> {
        return new Promise((resolve) => {
            setTimeout(() => {
            const dummyUser = [{id: 1, name: "Alan"},
                {id: 2, name: "Mark"}
            ];

            resolve(dummyUser);
        }, 1000);
        })
    }
}