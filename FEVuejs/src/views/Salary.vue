<template>
  <div>Welcome to Dashboard</div>
</template>
<script>




export default {
  name: "Salary",
  data() {
    return {
      currentday: new Date(),
      listStore: [],
      storeRequest: [],
      isLoading: 0,
      posititon: 0,
      modalProcess: false,
      locale: [
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec"
      ],
    
      nameprocess: "",
      datarq: {
        monthprcess: 1,
        yearprcess: 2019,
        groupstore:0
      },
      listYearProcees: [],
      listMonthProcees: [],
      showProcess: []
    };
  },
  created() {
    this.hideLoading();
    this.getYearProcess();
  },
  mounted() {
     this.hideLoading();
  },
  methods: {
    async fetch() {
      // const { data } = await userreponsitory.get();
      // this.user = data;
    },
    customFormatter(date) {
      return moment(date).format("DD-MM-YYYY");
    },
    async getStoreByProcess() {
      var self = this;
      this.showLoading();
      await storereponsitory
        .getStoreWithVariable(this.datarq)
        .then(function(response) {
          if (response.data.success) {
            self.listStore = response.data.data;
            self.showProcess=[];
            for (var i in self.listStore) {
              if (self.listStore[i].storeprocess.status == 0) {
                self.showProcess.push({
                  start: true,
                  process: false,
                  endprocess: false
                });
              } else {
                self.showProcess.push({
                  start: false,
                  process: false,
                  endprocess: true
                });
              };
              for (var j in self.listStore[i].variable) {
                self.listStore[i].variable[j] = {
                  variableid: self.listStore[i].variable[j].variableid,
                  name: self.listStore[i].variable[j].name,
                  typevalue: self.listStore[i].variable[j].typevalue,
                  status: self.listStore[i].variable[j].status,
                  storeid: self.listStore[i].variable[j].storeid,
                  storename: self.listStore[i].store.storename,
                  stdata: "",
                  number: 0,
                  datedata: self.currentday
                };
              }
            }
            self.hideLoading();
          }
        });
    },
    async executeStoreProcedure(id, processid, pos) {
      var self = this;
      self.updateViewButton(1, pos);
      var listDataReQuest = [];
      //add variable to list
      for (var i in this.listStore) {
        if (this.listStore[i].store.storeid == id) {
          for (var j in this.listStore[i].variable) {
            listDataReQuest.push({
              variableid: this.listStore[i].variable[j].variableid,
              typevalue: this.listStore[i].variable[j].typevalue,
              name: this.listStore[i].variable[j].name,
              stdata: this.listStore[i].variable[j].stdata,
              number: this.listStore[i].variable[j].number,
              datedata: this.listStore[i].variable[j].datedata
            });
          }
        }
      }
      //variable request
      var data = {
        storeid: id,
        processid: processid,
        storename: "",
        listvariable: listDataReQuest,
        position: this.listStore[pos].store.process
      };
      //call api
      await salaryreponsitory
        .execute(data)
        .then(function(response) {
          if (response.data.success) {
            self.updateViewButton(2, pos);
            self.listStore[pos].storeprocess.status = 2;
            $("#process").hide();
          } else {
            self.updateViewButton(0, pos);
            $("#process").hide();
            bootbox.alert(
              "Execure not successed, please check your store procedure!"
            );
          }
        })
        .catch(error => {
          self.updateViewButton(0, pos);
          bootbox.alert(
            "Execure the store procedure failed. Please check variables, the store procedure or connect to VPN." +
              error.error
          );
        });
    },
    restartProcess(id, processid,pos) {
      var self = this;
      var data = {
        "storeid": id,
        "processid": processid
      };
      bootbox.confirm({
        message: "Do you want to restart?",
        buttons: {
          confirm: {
            label: "Yes",
            className: "btn-success"
          },
          cancel: {
            label: "No",
            className: "btn-danger"
          }
        },
        callback: function(result) {
          if (result) {
            salaryreponsitory.restartProcess(data).then(function(response) {
              self.modalProcess = false;
              if (response.data.success) {
                self.$notify({
                  group: "foo-success",
                  title: "Notify",
                  type: "success",
                  text: "Restart success!"
                });
                self.updateViewButton(0,pos);
                self.listStore[pos].storeprocess.status=0;
              } else {
                bootbox.alert(response.data.message);
              }
            });
          }
        }
      });
    },
    updateViewButton(type, position) {
      //console.log("here");
      switch (type) {
        case 0:
          {
            this.showProcess[position].start = true;
            this.showProcess[position].process = false;
            this.showProcess[position].endprocess = false;
          }
          break;
        case 1:
          {
            this.showProcess[position].start = false;
            this.showProcess[position].process = true;
            this.showProcess[position].endprocess = false;
          }
          break;
        case 2:
          {
            this.showProcess[position].start = false;
            this.showProcess[position].process = false;
            this.showProcess[position].endprocess = true;
          }
          break;
      }
    },
    createSalaryProcess() {
      var storeprocess = {
        time: this.month.format("YYYY-MM-DD"),
        processname: this.nameprocess
      };
      // console.log(storeprocess);
      // return;
      var self = this;
      processreponsitory
        .createProcess(storeprocess)
        .then(function(response) {
          self.modalProcess = false;
          if (response.data.success) {
            self.$notify({
              group: "foo-success",
              title: "Notify",
              type: "success",
              text: "Please complete all information!"
            });
            self.getYearProcess();
          } else {
            bootbox.alert(response.data.message);
          }
        });
    },
    showDate(date) {
      this.date = date;
    },
    getYearProcess() {
      var self = this;
      storereponsitory
        .getYearProcess()
        .then(function(response) {
          if (response.data.success && response.data.data.length > 0) {
            self.listYearProcees = response.data.data;
            self.datarq.yearprcess = self.listYearProcees[0].year;
            self.getMonthProcess(self.listYearProcees[0].year);
          } else {
            self.hideLoading();
            bootbox.alert(
              "No processes have been initialized, Please click New Process"
            );
          }
        })
        .catch(error => {
          self.hideLoading();
        });
    },
    getMonthProcess(year) {
      var self = this;
      storereponsitory.getMonthProcess(year).then(function(response) {
         self.hideLoading();
        if (response.data.success) {
          self.listMonthProcees = response.data.data;
          self.datarq.monthprcess = self.listMonthProcees[0].month;
        } else {
          bootbox.alert(response.data.message);
        }
      });
      this.hideLoading();
    },
    showLoading() {
      $(".loading").show(100);
    },
    hideLoading() {
      $(".loading").hide(100);
    },
    sleep(ms) {
      return new Promise(resolve => setTimeout(resolve, ms));
    }
  },
 
};
</script>
<style scoped>


@media only screen and (min-width:0px){
    .k{
        position:relative;
        float:left;
    }
    .main-form{
        width:100%;
    }
    .form-title{
      width:100%;
      border: 1px solid rgba(51,51,51,0.1);
      padding: 5px;
      margin-bottom: 20px;
    }
    .title {
        color: var(--color-title);
        font-size: var(--fs-title);
        width: auto;
        margin-top: 5px;
        padding: 10px;
        font-weight: 600;
        
    }
    .month-process {
      color: var(--color-title);
      font-size: var(--fs-title);
      width: auto;
      margin-top: 5px;
      padding: 10px;
      font-weight: 300;
      
  }
  .ic-hand{
    width: 30px;
    height: 30px;
    margin-top: 9px;
    margin-left: 10px;
    background-repeat: no-repeat;
    background-size: contain;
 
    cursor: pointer;
  }
    .select-process {
      border:1px solid rgba(51,51,51,0.1);
      border-radius: 4px;
      padding: 5px;
      margin-top: 10px;
      cursor: pointer;
    }
    /*-------------------------------------------button------------------*/
    strong {
        font-weight: bold;
    }

    .bnt-callapi {
        background-color: #f2f2f2;
        background-image: linear-gradient(to bottom, #f2f2f2, #f2f2f2);
        border: 1px solid #bfbfbf;
        box-shadow: inset 0 1px 0 white, inset 0 -1px 0 #d9d9d9, inset 0 0 0 1px #f2f2f2, 0 2px 4px rgba(0, 0, 0, 0.2);
        color: #8c8c8c;
        text-shadow: 0 1px 0 rgba(255, 255, 255, 0.5);
        border-radius: 3px;
        cursor: pointer;
        display: inline-block;
        font-family: Verdana, sans-serif;
        font-size: 12px;
        font-weight: 400;
        line-height: 20px;
        padding: 5px 16px 5px;
        margin: 10px 0 0 16px;
        transition: all 20ms ease-out;
        vertical-align: top;
        position: relative;
        float: right;
        
    }

    .button-green {
        background-color: #97cc76;
        background-image: linear-gradient(to bottom, #97cc76, #8bcc62);
        border: 1px solid #5f993a;
        box-shadow: inset 0 1px 0 #c6e6b3, inset 0 -1px 0 #79b356, inset 0 0 0 1px #a4cc8b, 0 2px 4px rgba(0, 0, 0, 0.2);
        color: white;
        text-shadow: 0 1px 2px rgba(0, 0, 0, 0.3);
    }

        .button-green:hover, .button-green:focus {
            background: #8bcc62;
            border-color: #326612;
            box-shadow: inset 0 1px 0 #c6e6b3, inset 0 -1px 0 #79b356, inset 0 0 0 1px #a4cc8b;
        }

        .button-green:active {
            background: #8bcc62;
            box-shadow: inset 0 2px 3px rgba(0, 0, 0, 0.2);
        }

        .button-green .fa {
            color: #5f993a;
            text-shadow: 0 1px 0 rgba(255, 255, 255, 0.3);
        }

    .button .fa {
        float: left;
        font-size: 14px;
        line-height: 20px;
        margin: -1px 8px 0 -4px;
        vertical-align: top;
    }  

/*===== Vertical Timeline =====*/
#conference-timeline {
    position: relative;
    max-width: 920px;
    width: 100%;
    margin: 0 auto;
    margin-left: 20px;
  }
  #conference-timeline .timeline-start,
  #conference-timeline .timeline-end {
    display: table;
    font-family: "Roboto", sans-serif;
    font-size: 18px;
    font-weight: 900;
    text-transform: uppercase;
    background: #00b0bd;
    padding: 15px 23px;
    color: #fff;
    max-width: 5%;
    width: 100%;
    text-align: center;
    margin: 0 auto;
    cursor: pointer;
  }
  #conference-timeline .conference-center-line {
    position: absolute;
    width: 3px;
    height: 100%;
    top: 0;
    left: 50%;
    margin-left: -2px;
    background: #00b0bd;
    z-index: -1;
  }
  #conference-timeline .conference-timeline-content {
    padding-top: 67px;
    padding-bottom: 67px;
  }
  .timeline-article {
    width: 100%;
    height: 100%;
    position: relative;
    overflow: hidden;
    margin: 20px 0;
    margin-bottom: 50px;
  }
  .timeline-article .content-left-container,
  .timeline-article .content-right-container {
    max-width: 44%;
    width: 100%;
  }
  .timeline-article .timeline-author {
    display: block;
    font-weight: 400;
    font-size: 14px;
    line-height: 24px;
    color: #242424;
    text-align: right;
  }
  .timeline-article .content-left,
  .timeline-article .content-right {
    position: relative;
    float: left;
    width: 100%;
    border: 1px solid #ddd;
    background-color: #fff;
    box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
    transition: 0.3s;
    /* box-shadow: 0 1px 3px rgba(0,0,0,.03); */
    padding: 10px;
  }
  .content-body {
    margin: 0 0 0 0;
    padding: 0;
    font-weight: 400;
    color: #242424;
    font-size: 14px;
    line-height: 24px;
    position: relative;
    float: left;
    width: 100%;
  }
  .article-number {
    position: absolute;
    font-weight: 300;
    font-size: 44px;
    top: 10px;
    left: -60px;
    color: #00b0bd;
  }
  .timeline-article .content-left-container {
    float: left;
  }
  .timeline-article .content-right-container {
    float: right;
  }
  .timeline-article .content-left:before,
  .timeline-article .content-right:before{
    position: absolute;
    top: 20px;
    font-size: 23px;
    font-family: "FontAwesome";
    color: #fff;
  }
  .timeline-article .content-left:before {
    content: "\f0da";
    right: -8px;
  }
  .timeline-article .content-right:before {
    content: "\f0d9";
    left: -8px;
  }
  .timeline-article .meta-date {
    position: absolute;
    top: 0;
    left: 50%;
    width: 62px;
    height: 62px;
    margin-left: -31px;
    color: #fff;
    border-radius: 100%;
    background: #00b0bd;
    cursor: pointer;
  }
  .timeline-article .meta-date .date,
  .timeline-article .meta-date .month {
    display: block;
    text-align: center;
    font-weight: 900;
    margin-top: 9px;
  }
  .timeline-article .meta-date .date {
    font-size: 30px;
    line-height: 40px;
  }
  .timeline-article .meta-date .month {
    font-size: 18px;
    line-height: 10px;
  }
  .item-row{
    width:100%;
    border:1px solid rgba(51,51,51,0.05);
    padding: 5px;
  }
  .des-tt{
    color: var(--color);
    font-size: var(--fs-detail);
    padding: 5px;
    width: 30%;

  }
 .ip-tt{
  border:1px solid rgba(51,51,51,0.05);
  color: var(--color);
  font-size: var(--fs-detail);
  padding: 5px;
  width: 70%;
  border-radius: 4px;
 }
 .date-time{
  margin-top: 5px;
  border:1px solid rgba(51,51,51,0.1);
  border-radius: 4px;
  text-align: center;
  width: 70%;
}
.date-input{
  position: relative;
  float: left;
  margin-top: 5px;
  border:1px solid rgba(51,51,51,0.05);
  border-radius: 4px;
  text-align: center;
  padding: 5px;
  width: 70% !important;
}
.date-input-sl-month {
  position: relative;
  float: left;
  border-radius: 4px;
  text-align: center;
  width: 70%;
}
.vdp-datepicker>div:first-of-type {
  display:inline;
}
.item-right-body{
  width:100%;
  min-height: 40px;
  border-bottom:1px solid rgba(51,51,51,0.05);
  padding: 5px;
}
.bnt_restart{
  padding: 2px 10px;
  background-color: #97cc76;
    background-image: -webkit-gradient(linear, left top, left bottom, from(#97cc76), to(#8bcc62));
    background-image: linear-gradient(to bottom, #97cc76, #8bcc62);
    border: 1px solid #5f993a;
    -webkit-box-shadow: inset 0 1px 0 #c6e6b3, inset 0 -1px 0 #79b356, inset 0 0 0 1px #a4cc8b, 0 2px 4px rgba(0, 0, 0, 0.2);
    box-shadow: inset 0 1px 0 #c6e6b3, inset 0 -1px 0 #79b356, inset 0 0 0 1px #a4cc8b, 0 2px 4px rgba(0, 0, 0, 0.2);
    color: white;
    text-shadow: 0 1px 2px rgba(0, 0, 0, 0.3);
    cursor: pointer;
    border-radius: 3px;
}
.content-right-item{
  width: 20%;
  margin-bottom: 5px;
  word-break: break-all;
}
.boil-content{
  font-weight: 600;
  position: relative;
  float: left;
  width: 80%;
  word-break: break-all;
}
  /*===== // Vertical Timeline =====*/

  /*loading*/
  .loading {
    position: fixed;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    z-index: 10000000;
    opacity: 0.8;
   
    }
    .loading-button{
      text-align: center;
      font-size: 40px;
      margin: auto;
      color: orange;
      width: 100%;
      top: 0;
      bottom: 0;
      left: 0;
      right: 0;
      position: absolute;
    }
    .loading-button1{
      text-align: center;
      font-size: 40px;
      margin: auto;
      color: orange;
      width: 100%;
      top: 0;
      bottom: 0;
      left: 0;
      right: 0;
      position: absolute;
    }
  /* modal update  */
  .modal-body{
    width:100%;
    padding:10px;
    padding-top:0;
}
.modal-title {
    color: var(--color-title);
    font-size: 25px;
}
.modal-content{
    width: 100%;
    border:none;
}
.dl-sm-it{
    width:100%;
    margin-bottom: 10px;
}
.dl-name-fml{
    width:30%;
}
.dl-input-fml{
    width:70%;
    border:1px solid rgba(51,51,51,0.1);
    border-radius:4px;
    padding:5px;
}
.bnt-accept {
    width: 100%;
    background-color: #97cc76;
    background-image: linear-gradient(to bottom, #97cc76, #8bcc62);
    border: 1px solid #5f993a;
    box-shadow: inset 0 1px 0 #c6e6b3, inset 0 -1px 0 #79b356, inset 0 0 0 1px #a4cc8b, 0 2px 4px rgba(0, 0, 0, 0.2);
    color: white;
    margin:unset;
    text-align:center;
    font-size:var(--fs-title);
    cursor: pointer;
    font-weight: 600;
    margin-top: 20px;
 }
   /* notifi */
   .vue-notification {
    padding: 10px;
    margin: 0 5px 5px;
    font-size: 12px;
    color: #ffffff;
    background: #44A4FC;
    border-left: 5px solid #187FE7;
}
.warn {
     background: #ffb648;
     border-left-color: #f48a06;
}

.error {
    background: #E54D42;
    border-left-color: #B82E24;
}

.success {
    background: #68CD86;
    border-left-color: #42A85F;
}
}


 /*===== Resonsive Vertical Timeline =====*/
 @media only screen and (max-width: 830px) {
    #conference-timeline .timeline-start,
    #conference-timeline .timeline-end {
      margin: 0;
    }
    #conference-timeline .conference-center-line {
      margin-left: 0;
      left: 50px;
    }
    .timeline-article .meta-date {
      margin-left: 0;
      left: 20px;
    }
    .timeline-article .content-left-container,
    .timeline-article .content-right-container {
      max-width: 100%;
      width: auto;
      float: none;
      margin-left: 110px;
      min-height: 53px;
    }
    .timeline-article .content-left-container {
      margin-bottom: 20px;
    }
    .timeline-article .content-left,
    .timeline-article .content-right {
      padding: 10px 25px;
      min-height: 65px;
      
    }
    .timeline-article .content-left:before {
      content: "\f0d9";
      right: auto;
      left: -8px;
    }
    .timeline-article .content-right:before {
      display: none;
    }
  }
  @media only screen and (max-width: 400px) {
    .content-body {
      margin: 0;
    }
    .article-number {
      display: none;
    }
    
  }
  /*===== // Resonsive Vertical Timeline =====*/
@media only screen and (min-width:1200px) {  
}
</style>