import { Pipe, PipeTransform } from '@angular/core';
//import Eventmodel = require("./Models/event-model");
//const format = require('date-fns/format');

@Pipe({
  name: 'customTime'
})
export class CustomTimePipe implements PipeTransform {

  transform(value:any, args?: any): any {
    return null;
  }

}
