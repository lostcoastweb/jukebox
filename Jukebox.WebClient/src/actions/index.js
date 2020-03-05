//Taken from another project to be updated as needed for this one
import { UPDATE_USER } from "./constants";

export function updateUser(payload) {
  return { type: UPDATE_USER, payload };
}