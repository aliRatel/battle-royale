var app = require('express')();
var server = require('http').Server(app);
const bigInt = require('big-integer');
var aes1 = require("./AES.js");
var io = require('socket.io')(server);
var sql = require('mysql');
var CryptoJS = require("crypto-js/aes");
var Rsa = require('./RSA.js');
var rsaKeys = Rsa.generate(256);

function RsaEcr(plain){
    return  Rsa.encrypt(Rsa.encode(plain),rsaKeys.n,rsaKeys.e);
}

function RsaDec(cipher){
 var cipher = bigInt(Rsa.decrypt(cipher,rsaKeys.d,rsaKeys.n));

  return Rsa.decode(cipher);
}
server.listen(4000);

//global variables for the sever
var date = new Date();
var publicAesKey = date.toDateString()+date.toTimeString();
publicAesKey = publicAesKey.replace(/\s/g, '');
publicAesKey = publicAesKey.substr(5,16);
var aes = new aes1(publicAesKey);
var a =aes1.encrypting('asfasd');






    // console.log(AES.initKey);
var zoneSize = 1;
var config = {
    time: 1,
    startTime: null,
    totalTime: (60000)* 5,
    totalPlayers: 3,
    status: 'ready'
};
var plain = {
    position: [],
    rotation: [],
    players: 0,
    status: 'fixed'
}   ;
var sessionRoom = 'room';
var status;
var playerSpawnPoint = [];
var clients = [];
var players = [];
var weapons=[];
var totalPlayers = 0;
var items=['ak-47','m4a1'];

app.get('/', function (req, res) {
    res.send('server on ');
});
//todo: set enc key and params
//todo: generate session params
//todo: wait for players
//todo: start session
//data base
var con = sql.createConnection({
    host: 'localhost',
    user: 'root',
    password: '',
    port: 3306,
    database: 'gameDB'
});

con.connect(function (err) {
    if (!err)
        console.log("db Connected!");
    else     console.log(+err);

    con.query("CREATE DATABASE gameDB", function (err, result) {
        if (!err)
            console.log("Database created");
        // else     console.log(err);

    });
});
//

io.on('connection', function (socket) {
    console.log("connectionnnnn");
    var rsa ={d:rsaKeys.d,N:rsaKeys.n,cipher:RsaEcr(publicAesKey)};
    socket.emit('rsa',rsa);


    socket.on("weapons points",function (data) {
        var weaponSpawnPoint = [];
        for(var i = 0  ;i<data.p.length;i++){
            var spawnPoint = {position:data.p[i].position};
            console.log(spawnPoint);
            weaponSpawnPoint.push(spawnPoint);
            var query = "insert into weapon_spawn_point (id,x,y,z) values('" +
                i + "','" + spawnPoint.position[0] + "','"
                + spawnPoint.position[1]+ "','"
                + spawnPoint.position[2]+ "')";
            con.query(query, function (err, result) {
                if (err) {

                    console.log(err);
                }
                else {
                    console.log(result);
                }

            });
        }

    });







    socket.on('sign up', function (data) {
        addNewUser(data, socket);
        console.log(data);
    });
    socket.on('log in', function (data) {
        console.log('ci  '+JSON.stringify(data));
        var info = JSON.parse(aes1.decrypting(data.s));
        console.log(info);
        logInUser(info, socket);
    });
    socket.on('play', function (data) {
        if (config.status == 'in game') {
            console.log('in game');
            socket.emit('try again');
            return;
        }else{
            console.log('ready');

            socket.emit('can join');
        }

        if (config.status != 'loppy') {
            config.status = 'loppy';
        }
        socket.join(sessionRoom);
        var player = {
            health: 100,
            sessionId: totalPlayers,
            position: [],
            rotation: [],
            socketId: socket.id,
            status: 'loppy'
        };
        console.log(socket.id);
        totalPlayers++;
        players.push(player);
        console.log(players);
        var i = getclientBySocket(socket);

            clients[i].playerid = player.sessionId;


    });


    socket.on('join session', function (data) {
        var i = findId(socket);

        players[i].rotation = data.rotation;
        players[i].position = data.position;
        console.log("i: " + i);
        var id = players[i].sessionId;
        var c = (players[i]) ;
        socket.emit('setId', players[i]  );


        console.log(data);


    });

    socket.on('id is set', function (data) {
        var i = findId(socket);

        socket.emit('approved', {players});
        socket.broadcast.to(sessionRoom).emit('other player connected', players[i]);

        if (totalPlayers == config.totalPlayers) {
            config.status = 'in game';
            generateWeapons();

            // config.startTime = date.getMilliseconds();
            setTimeout(function() {
                io.sockets.in(sessionRoom).emit('start game');
            }, 5000);

        }

    });
    socket.on('in scene', function () {
        console.log('in scene');
        if (plain.players > totalPlayers)
            return;
var i = findId(socket);
        players[i].status = 'in plain';
        plain.players++;



        if (plain.players == totalPlayers) {
            plain.status = "move";
            config.startTime = Date.now();
            console.log(config.startTime);
            zoneSize = 100;
            setInterval(decreaseZone,2000);
            io.sockets.in(sessionRoom).emit('move plain',{weapons});
           //io.sockets.in(sessionRoom).emit('weapons');

        }
    });
    socket.on('parachute', function (data) {
        var i = findId(socket);
        console.log(socket.id);
        players[data.sessionId].status = 'air born';
        plain.players--;
        socket.broadcast.to(sessionRoom).emit('player air born', data);
    });
    socket.on('land', function (data) {
        socket.broadcast.to(sessionRoom).emit('player landed', data);
    });
    socket.on('kick players',function () {
        if(plain.players==0)
            return;

       for( var i = 0 ; i<players.length;i++)
           if (players[i].status== 'in plain'){
           console.log(players[i].sessionId);
           io.to(players[i].socketId).emit('jump');
           }
    });
    socket.on('flash muzzle',function(data){
       socket.broadcast.to(sessionRoom).emit('flash muzzle',data);
    });
    socket.on('hit player', function (data) {
if(players[data.playerId].health<=0)players[data.playerId].status='dead';
console.log(data);
       players[data.playerId].health -=data.health;

        if(players[data.playerId].health <=0){
            console.log("kill");
            players[data.playerId].status = 'dead';
            console.log(players);
            io.sockets.in(sessionRoom).emit('kill player',data);
            var isWinner = checkWinner();
            if(isWinner != -1){
                console.log("we have a winner   "+players[isWinner].socketId);
                io.to(players[isWinner].socketId).emit('you won',{s:'asdf'});
                resetServer();

            }
        }else{
            console.log("hit");

            var da = {
                newHealth:players[data.playerId].health,
                id:data.playerId
            };
            io.sockets.in(sessionRoom).emit('hit player',da);
        }



    });
    socket.on('weapon changed', function (data) {

        socket.broadcast.to(sessionRoom).emit('weapon changed', data);

        console.log(JSON.stringify(data));

    });
    socket.on('player moved', function (data) {
        if(players[data.sessionId]==null) return;

        players[data.sessionId].position = data.position.slice();
        socket.broadcast.emit('player moved', data);
    });
    socket.on('player rotated', function (data) {
        console.log(data);
        if(players[data.sessionId]==null) return;
        players[data.sessionId].rotation = data.rotation;
        socket.broadcast.emit('player rotated', data);
    });
    socket.on('player animated', function (data) {
        if(players[data.sessionId]==null) return;

        socket.broadcast.emit('player animated', data);
    });
    socket.on('got in game scene', function () {
        socket.broadcast.to(sessionRoom).emit('put in plain');
    });
    socket.on('disconnect', function () {
        var i = getclientBySocket(socket);
        if (i != -1)
            clients.splice(i);
    });
});


console.log('---server is running ...');

function getclientBySocket(socket) {
    for (var i = 0; i < clients.length; i++)
        if (clients[i].clientSocket == socket)
            return i;

    return -1;
}

function getclientBySessionId(id) {
    for (var i = 0; i < clients.length; i++)
        if (clients[i].player.sessionId == id)
            return clients[i];

    return null;
}

function addNewUser(data, socket) {
    console.log(data.password);
    var query = "insert into user (user_name,password,rank,total_kills,access_token) values('" +
        data.userName + "','" + data.password + "',0,0,1)";
    con.query(query, function (err, result) {
        if (!err)
            socket.emit("user name already exists");
        else
            console.log(err);
    });

}
function logInUser(data, socket) {
    console.log("in func  " +data.userName + "  " + data.password);
    var query = "select access_token,id from user where( upper(user_name) = upper('"
        + data.userName + "') and password = '" + data.password + "')";
    con.query(query, function (err, result) {
        if (err) {

            console.log(err);
        }
        else {
            console.log(result);
            if (result.length == 0) {
                socket.emit("wrong user name or password");
                return;
            }
            var row = result[0];
            var s = row.access_token;
            if (s == 0)socket.emit("logged in from another device ");
            else {
                socket.emit("logged in successed");
                query = "update user set access_token = 0 where id = '" + row.id + "'";
                var client = {
                    status: 'connected',
                    clientSocket: socket,
                    playerid: null,
                    dbId: row.id
                };
                clients.push(client);
            }
        }
    });

}

function findId(socket) {
    for (var i = 0; i < players.length; i++)
        if (socket.id == players[i].socketId)
            return i;
    return -1;
}

function checkGameStatus() {
// if(date.getMilliseconds()>config.endTime){
//     config.startTime=null;
//     config.endTime=null;
//     totalPlayers=0;
//     players.empty();
//     // socket._rooms.empty();
//
//     for (var i =0 ;i<clients.length;i++){
//         client[i].playerid=null;
//     }
// }

    io.sockets.in(sessionRoom).emit('decrease zone');
}
function generateWeapons(){

    for(var i=0;i<336;i++){
        weapon = {
            name:randomItemName(),
            id :i

        }
        weapons.push(weapon);
    }

}
function randomItemName (){
    min = Math.ceil(0);
    max = Math.floor(items.length);
    var ran= Math.floor(Math.random() * (max - min)) + min;
    return items[ran];
}
function startGame() {
    console.log('start game started');
    io.sockets.in(sessionRoom).emit('start game');
    console.log('start game ended');

}

function decreaseZone() {
    var timeLapse = Date.now()-config.startTime;
     zoneSize = timeLapse/config.totalTime;
    if(zoneSize == 0.99) return;

    var zone = { size :zoneSize};

io.sockets.in(sessionRoom).emit('decrease zone',zone);
}
function checkWinner() {
    var winnderId=-1;
    var alive = 0 ;
    for(var i = 0;i<players.length;i++){
        if(players[i].status != 'dead') {
            winnderId = i;
            alive++;
        }
        if(alive>1){
            return -1;
        }
    }
    return winnderId;
}
function resetServer() {
    players=[];
    config.startTime = null;
    config.status= "ready";
    weapons=[];
    totalPlayers = 0;
    plain = {
        position: [],
        rotation: [],
        players: 0,
        status: 'fixed'
    };
    io.of('/').in(sessionRoom).clients(function(error, clients) {
        if (clients.length > 0) {
            console.log('clients in the room: \n');
            console.log(clients);
            clients.forEach(function (socket_id) {
                io.sockets.sockets[socket_id].leave(sessionRoom);
            });
        }
    });
}
