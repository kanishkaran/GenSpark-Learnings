import { UserModel } from "../models/UserModel";


export interface UserState {
    users: UserModel[];
    loading: boolean;
    error: string | null;
}

export const initialState : UserState = {
    users: [],
    loading: false,
    error: null
}