import { createAction, props } from "@ngrx/store";
import { UserModel } from "../models/UserModel";


export const loadUsers = createAction('[Users] Load Users');
export const loadUsersSuccess = createAction('[Users] Load User Success', props<{ users: UserModel[] }>());
export const addUser = createAction('[Users] Add User', props<{ user: UserModel }>());
export const loadUsersFailure = createAction('[User] Load Users Failure', props<{ error: string }>());