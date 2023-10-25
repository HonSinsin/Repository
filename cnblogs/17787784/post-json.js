/* 音频转文字 --> 使用 JSON 方式传递 */
const axios = require("axios").default;
const access_token = "ur access_token"
const cuid = "ur cuid"

async function main() {
  var headers = { 'Content-Type': 'application/json', }
  var url = 'https://vop.baidu.com/server_api'
  var options = JSON.stringify({
    "format": "pcm",
    "rate": 16000,
    "channel": 1,
    "cuid": cuid,
    "token": access_token,
    "speech": getFileContentAsBase64("E:/xxx/JianWangChao.wav"),
    "len": 274832,
  });

  axios.post(url, options, headers
  ).then(function (response) {
    console.log(response.data);
  }).catch(function (err) {
    console.log("#>>  catch", err);
  });
}

/** 获取文件base64编码 */
function getFileContentAsBase64(path) {
  const fs = require('fs');
  try {
    var base64Str = fs.readFileSync(path, { encoding: 'base64' })
    return base64Str;
  } catch (err) { throw new Error(err); }
}
main();
