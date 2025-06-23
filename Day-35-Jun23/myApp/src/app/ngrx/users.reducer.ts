import { createReducer, on } from "@ngrx/store";
import { initialState } from "./userState";
import * as UserActions from './users.action'


export const userReducer = createReducer(initialState, 
    on(UserActions.loadUsers, state => ({...state, loading: true, error: null})),
    on(UserActions.loadUsersSuccess, (state, {users}) => ({...state, users, loading: false, error: null})),
    on(UserActions.addUser, (state, {user}) => ({...state, users: [...state.users, user], loading: false, error: null})),
    on(UserActions.loadUsersFailure, (state, {error}) => ({...state, loading: false, error}))
)