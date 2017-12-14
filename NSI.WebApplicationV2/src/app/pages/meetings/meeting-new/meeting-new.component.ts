import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Meeting } from './meeting';
import { MeetingsService } from '../../../services/meetings.service';
import { UsersService } from '../../../services/users.service';
import { ActivatedRoute, Router } from '@angular/router';

declare var $: any;

@Component({
  selector: 'app-meeting-new',
  templateUrl: './meeting-new.component.html',
  styleUrls: ['./meeting-new.component.scss']
})
export class MeetingNewComponent implements OnInit, AfterViewInit {

  ngAfterViewInit(): void {
    let self = this;
    $('#from').datetimepicker({
      format: "MM/DD/YYYY, hh:mm:ss"
    });
    $('#to').datetimepicker({
      useCurrent: false, //Important! See issue #1075
      format: "MM/DD/YYYY, hh:mm:ss"
    });
    $("#from").on("dp.change", function (e: any) {
      self.model.from = $("#from").val();
      $('#to').data("DateTimePicker").minDate(e.date);
    });
    $("#to").on("dp.change", function (e: any) {
      self.model.to = $("#to").val();
      $('#from').data("DateTimePicker").maxDate(e.date);
    });
  }

  query: string;
  filteredList: string[];
  model: Meeting;
  id: number;
  public edit: boolean = false;

  constructor(private meetingsService: MeetingsService, private usersService: UsersService, private route: ActivatedRoute,
              private router: Router) {
    this.query = '';
    this.filteredList = [];
    this.model = new Meeting();
  }


  filter() {
    if (this.query.length > 2) {
      this.usersService.getForMeetings(this.query).subscribe(data => {
        console.log(data);
        this.filteredList = data;
      })
    } else {
      this.filteredList = [];
    }
  }

  add(item: string) {
    this.model.userMeeting.push(item);
    this.query = '';
    this.filteredList = [];
  }

  remove(item: string) {
    this.model.userMeeting.splice(this.model.userMeeting.indexOf(item), 1);
  }

  onSubmit() {
    console.log(this.model);
    this.model.from = $('#from').val();
    this.model.to = $('#to').val();
    console.log(this.model);
    this.meetingsService.postMeeting(this.model).subscribe((r: any) => this.router.navigate(['/meetings']),
      (error: any) => console.log("Error: " + error.message));

  }

  newMeeting() {
    this.model = new Meeting();
    this.router.navigate(['/meetings/new']);
  }

  //edit-update
  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id');
    console.log(this.id);
    if (this.id != 0) {
      this.meetingsService.getMeetingById(this.id).subscribe(response => {
        let data = response.data;
        if (data != null) {
          this.edit = true;
          console.log(data);
          this.model.title = data.title;
          this.model.meetingPlace = data.meetingPlace;
          this.model.from = new Date(data.from).toLocaleString();
          this.model.to = new Date(data.to).toLocaleString();
          this.model.userMeeting = data.userMeeting;
        }
        console.log(this.edit);
      });
    }
    let dateFrom = this.route.snapshot.queryParamMap.get("dateFrom");
    let dateTo = this.route.snapshot.queryParamMap.get("dateTo");

    if (dateFrom && dateTo) {
      this.model.from = new Date(Number.parseInt(dateFrom)).toLocaleString();
      this.model.to = new Date(Number.parseInt(dateTo)).toLocaleString();
    }
  }

  updateMeeting() {
    this.model.from = $('#from').val();
    this.model.to = $('#to').val();
    console.log(this.model);

    this.meetingsService.putMeeting(this.id, this.model).subscribe((r: any) => this.router.navigate(['/meetings']),
      (error: any) => console.log("Error: " + error.message));

  }

  deleteMeeting() {
    console.log(this.id);
    this.meetingsService.deleteMeetingById(this.id).subscribe((r: any) => this.router.navigate(['/meetings']),
      (error: any) => console.log("Error: " + error.message));
  }
}
