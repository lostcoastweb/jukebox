//Taken from another project to be updated as needed for this one
import { UPDATE_USER, UPDATE_COURSE_USER } from "../actions/constants";
/*
import ConfigManager from '../config'
import User from "../models/User";
import Course from '../models/Course';
import Assignment from "../models/Assignment";
var config = ConfigManager.getConfig();
*/

const initialState = {
   current_user: { id: -1 }
};
function rootReducer(state = initialState, action) {
   if (action.type === UPDATE_USER) {
      return Object.assign({}, state, { current_user: action.payload });
   }
   return state;
};
export default rootReducer;