/* 音频转文字 --> 使用 RAW 方式传递 */
const axios = require("axios").default;
const access_token = "ur access_token"
const cuid = "ur cuid"

async function main() {
  var data = getFileContentAsBase64("E:/xxx/jiarenmen.wav")
  var url = 'https://vop.baidu.com/server_api?dev_pid=1537&cuid=' + cuid + '&token=' + access_token
  axios({
    url: url,
    method: "post",
    headers: { 'Content-Type': 'audio/wav;rate=16000' },
    data: data
  }).then((res) => {
    console.log('#> then, ', res.data)
  }, (err) => {
    console.log('#> err, ', err)
  })
}

/**
 * 获取文件base64编码
 * @param string  path 文件路径
 * @return string base64编码信息，不带文件头
 */
function getFileContentAsBase64(path) {
  const fs = require('fs');
  try {
    var base64Str = fs.readFileSync(path)
    return base64Str;
  } catch (err) {
    throw new Error(err);
  }
}
main();