/* Generated from Java with JSweet 2.2.0-SNAPSHOT - http://www.jsweet.org */


var Aes = (function () {
    function Aes(initkey) {
        Aes.key = initkey;
        Aes.generateKeys();
    }
    Aes.Rcon_$LI$ = function () { if (Aes.Rcon == null)
        Aes.Rcon = [[1, 2, 4, 8, 16, 32, 64, 128, 27, 54], [0, 0, 0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]]; return Aes.Rcon; };
    ;
    Aes.sBox_$LI$ = function () { if (Aes.sBox == null)
        Aes.sBox = [[(99 | 0), (124 | 0), (119 | 0), (123 | 0), (242 | 0), (107 | 0), (111 | 0), (197 | 0), (48 | 0), (1 | 0), (103 | 0), (43 | 0), (254 | 0), (215 | 0), (171 | 0), (118 | 0)], [(202 | 0), (130 | 0), (201 | 0), (125 | 0), (250 | 0), (89 | 0), (71 | 0), (240 | 0), (173 | 0), (212 | 0), (162 | 0), (175 | 0), (156 | 0), (164 | 0), (114 | 0), (192 | 0)], [(183 | 0), (253 | 0), (147 | 0), (38 | 0), (54 | 0), (63 | 0), (247 | 0), (204 | 0), (52 | 0), (165 | 0), (229 | 0), (241 | 0), (113 | 0), (216 | 0), (49 | 0), (21 | 0)], [(4 | 0), (199 | 0), (35 | 0), (195 | 0), (24 | 0), (150 | 0), (5 | 0), (154 | 0), (7 | 0), (18 | 0), (128 | 0), (226 | 0), (235 | 0), (39 | 0), (178 | 0), (117 | 0)], [(9 | 0), (131 | 0), (44 | 0), (26 | 0), (27 | 0), (110 | 0), (90 | 0), (160 | 0), (82 | 0), (59 | 0), (214 | 0), (179 | 0), (41 | 0), (227 | 0), (47 | 0), (132 | 0)], [(83 | 0), (209 | 0), (0 | 0), (237 | 0), (32 | 0), (252 | 0), (177 | 0), (91 | 0), (106 | 0), (203 | 0), (190 | 0), (57 | 0), (74 | 0), (76 | 0), (88 | 0), (207 | 0)], [(208 | 0), (239 | 0), (170 | 0), (251 | 0), (67 | 0), (77 | 0), (51 | 0), (133 | 0), (69 | 0), (249 | 0), (2 | 0), (127 | 0), (80 | 0), (60 | 0), (159 | 0), (168 | 0)], [(81 | 0), (163 | 0), (64 | 0), (143 | 0), (146 | 0), (157 | 0), (56 | 0), (245 | 0), (188 | 0), (182 | 0), (218 | 0), (33 | 0), (16 | 0), (255 | 0), (243 | 0), (210 | 0)], [(205 | 0), (12 | 0), (19 | 0), (236 | 0), (95 | 0), (151 | 0), (68 | 0), (23 | 0), (196 | 0), (167 | 0), (126 | 0), (61 | 0), (100 | 0), (93 | 0), (25 | 0), (115 | 0)], [(96 | 0), (129 | 0), (79 | 0), (220 | 0), (34 | 0), (42 | 0), (144 | 0), (136 | 0), (70 | 0), (238 | 0), (184 | 0), (20 | 0), (222 | 0), (94 | 0), (11 | 0), (219 | 0)], [(224 | 0), (50 | 0), (58 | 0), (10 | 0), (73 | 0), (6 | 0), (36 | 0), (92 | 0), (194 | 0), (211 | 0), (172 | 0), (98 | 0), (145 | 0), (149 | 0), (228 | 0), (121 | 0)], [(231 | 0), (200 | 0), (55 | 0), (109 | 0), (141 | 0), (213 | 0), (78 | 0), (169 | 0), (108 | 0), (86 | 0), (244 | 0), (234 | 0), (101 | 0), (122 | 0), (174 | 0), (8 | 0)], [(186 | 0), (120 | 0), (37 | 0), (46 | 0), (28 | 0), (166 | 0), (180 | 0), (198 | 0), (232 | 0), (221 | 0), (116 | 0), (31 | 0), (75 | 0), (189 | 0), (139 | 0), (138 | 0)], [(112 | 0), (62 | 0), (181 | 0), (102 | 0), (72 | 0), (3 | 0), (246 | 0), (14 | 0), (97 | 0), (53 | 0), (87 | 0), (185 | 0), (134 | 0), (193 | 0), (29 | 0), (158 | 0)], [(225 | 0), (248 | 0), (152 | 0), (17 | 0), (105 | 0), (217 | 0), (142 | 0), (148 | 0), (155 | 0), (30 | 0), (135 | 0), (233 | 0), (206 | 0), (85 | 0), (40 | 0), (223 | 0)], [(140 | 0), (161 | 0), (137 | 0), (13 | 0), (191 | 0), (230 | 0), (66 | 0), (104 | 0), (65 | 0), (153 | 0), (45 | 0), (15 | 0), (176 | 0), (84 | 0), (187 | 0), (22 | 0)]]; return Aes.sBox; };
    ;
    Aes.sBoxInv_$LI$ = function () { if (Aes.sBoxInv == null)
        Aes.sBoxInv = [[(82 | 0), (9 | 0), (106 | 0), (213 | 0), (48 | 0), (54 | 0), (165 | 0), (56 | 0), (191 | 0), (64 | 0), (163 | 0), (158 | 0), (129 | 0), (243 | 0), (215 | 0), (251 | 0)], [(124 | 0), (227 | 0), (57 | 0), (130 | 0), (155 | 0), (47 | 0), (255 | 0), (135 | 0), (52 | 0), (142 | 0), (67 | 0), (68 | 0), (196 | 0), (222 | 0), (233 | 0), (203 | 0)], [(84 | 0), (123 | 0), (148 | 0), (50 | 0), (166 | 0), (194 | 0), (35 | 0), (61 | 0), (238 | 0), (76 | 0), (149 | 0), (11 | 0), (66 | 0), (250 | 0), (195 | 0), (78 | 0)], [(8 | 0), (46 | 0), (161 | 0), (102 | 0), (40 | 0), (217 | 0), (36 | 0), (178 | 0), (118 | 0), (91 | 0), (162 | 0), (73 | 0), (109 | 0), (139 | 0), (209 | 0), (37 | 0)], [(114 | 0), (248 | 0), (246 | 0), (100 | 0), (134 | 0), (104 | 0), (152 | 0), (22 | 0), (212 | 0), (164 | 0), (92 | 0), (204 | 0), (93 | 0), (101 | 0), (182 | 0), (146 | 0)], [(108 | 0), (112 | 0), (72 | 0), (80 | 0), (253 | 0), (237 | 0), (185 | 0), (218 | 0), (94 | 0), (21 | 0), (70 | 0), (87 | 0), (167 | 0), (141 | 0), (157 | 0), (132 | 0)], [(144 | 0), (216 | 0), (171 | 0), (0 | 0), (140 | 0), (188 | 0), (211 | 0), (10 | 0), (247 | 0), (228 | 0), (88 | 0), (5 | 0), (184 | 0), (179 | 0), (69 | 0), (6 | 0)], [(208 | 0), (44 | 0), (30 | 0), (143 | 0), (202 | 0), (63 | 0), (15 | 0), (2 | 0), (193 | 0), (175 | 0), (189 | 0), (3 | 0), (1 | 0), (19 | 0), (138 | 0), (107 | 0)], [(58 | 0), (145 | 0), (17 | 0), (65 | 0), (79 | 0), (103 | 0), (220 | 0), (234 | 0), (151 | 0), (242 | 0), (207 | 0), (206 | 0), (240 | 0), (180 | 0), (230 | 0), (115 | 0)], [(150 | 0), (172 | 0), (116 | 0), (34 | 0), (231 | 0), (173 | 0), (53 | 0), (133 | 0), (226 | 0), (249 | 0), (55 | 0), (232 | 0), (28 | 0), (117 | 0), (223 | 0), (110 | 0)], [(71 | 0), (241 | 0), (26 | 0), (113 | 0), (29 | 0), (41 | 0), (197 | 0), (137 | 0), (111 | 0), (183 | 0), (98 | 0), (14 | 0), (170 | 0), (24 | 0), (190 | 0), (27 | 0)], [(252 | 0), (86 | 0), (62 | 0), (75 | 0), (198 | 0), (210 | 0), (121 | 0), (32 | 0), (154 | 0), (219 | 0), (192 | 0), (254 | 0), (120 | 0), (205 | 0), (90 | 0), (244 | 0)], [(31 | 0), (221 | 0), (168 | 0), (51 | 0), (136 | 0), (7 | 0), (199 | 0), (49 | 0), (177 | 0), (18 | 0), (16 | 0), (89 | 0), (39 | 0), (128 | 0), (236 | 0), (95 | 0)], [(96 | 0), (81 | 0), (127 | 0), (169 | 0), (25 | 0), (181 | 0), (74 | 0), (13 | 0), (45 | 0), (229 | 0), (122 | 0), (159 | 0), (147 | 0), (201 | 0), (156 | 0), (239 | 0)], [(160 | 0), (224 | 0), (59 | 0), (77 | 0), (174 | 0), (42 | 0), (245 | 0), (176 | 0), (200 | 0), (235 | 0), (187 | 0), (60 | 0), (131 | 0), (83 | 0), (153 | 0), (97 | 0)], [(23 | 0), (43 | 0), (4 | 0), (126 | 0), (186 | 0), (119 | 0), (214 | 0), (38 | 0), (225 | 0), (105 | 0), (20 | 0), (99 | 0), (85 | 0), (33 | 0), (12 | 0), (125 | 0)]]; return Aes.sBoxInv; };
    ;
    Aes.encrypt = function (byteMatrix) {
        var state = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([4, 4]);
        state = byteMatrix;
        Aes.addRoundKey(state, Aes.keys, 0);
        for (var round = 1; round < 10; round++) {
            {
                state = Aes.subBytes(state);
                state = Aes.shiftRows(state);
                state = Aes.mixColumns(state);
                Aes.addRoundKey(state, Aes.keys, round);
            }
            ;
        }
        state = Aes.subBytes(state);
        state = Aes.shiftRows(state);
        Aes.addRoundKey(state, Aes.keys, 10);
        return state;
    };
    Aes.setBlock = function (fullText, k) {
        var block = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([4, 4]);
        block = Aes.arrayToMatrix(fullText, k);
        return block;
    };
    Aes.arrayToMatrix = function (fullText, k) {
        var temp = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([4, 4]);
        for (var i = 0; i < 4; i++) {
            {
                for (var j = 0; j < 4; j++) {
                    {
                        temp[j][i] = fullText[k++];
                    }
                    ;
                }
            }
            ;
        }
        return temp;
    };
    Aes.decrypt = function (byteMatrix) {
        var state = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([4, 4]);
        state = byteMatrix;
        Aes.addRoundKey(state, Aes.keys, 10);
        state = Aes.invShiftRows(state);
        state = Aes.invSubBytes(state);
        for (var round = 9; round >= 1; round--) {
            {
                Aes.addRoundKey(state, Aes.keys, round);
                state = Aes.invMixColumns(state);
                state = Aes.invShiftRows(state);
                state = Aes.invSubBytes(state);
            }
            ;
        }
        Aes.addRoundKey(state, Aes.keys, 0);
        return state;
    };
    Aes.encrypting = function (plainText) {
        var fullText;
        if ((plainText.length % 16) === 0) {
            fullText = (function (s) { var a = []; while (s-- > 0)
                a.push(0); return a; })(plainText.length);
            fullText = (plainText).split('').map(function (s) { return s.charCodeAt(0); });
        }
        else {
            plainText = Aes.padding(plainText);
            fullText = (function (s) { var a = []; while (s-- > 0)
                a.push(0); return a; })(plainText.length);
            fullText = (plainText).split('').map(function (s) { return s.charCodeAt(0); });
        }
        var toReturn = "";
        var state = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([4, 4]);
        for (var i = 0; i < fullText.length; i += 16) {
            {
                state = Aes.setBlock(fullText, i);
                state = Aes.encrypt(state);
                toReturn += Aes.arrayToString(state);
            }
            ;
        }
        return toReturn;
    };
    Aes.decrypting = function (cipherText) {
        var temp = (function (s) { var a = []; while (s-- > 0)
            a.push(0); return a; })((cipherText.length / 2 | 0));
        for (var i = 0; i < temp.length; i++) {
            {
                var index = i * 2;
                var j = parseInt(cipherText.substring(index, index + 2), 16);
                temp[i] = (j | 0);
            }
            ;
        }
        var toReturn = "";
        var state = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([4, 4]);
        for (var i = 0; i < temp.length; i += 16) {
            {
                state = Aes.setBlock(temp, i);
                state = Aes.decrypt(state);
                toReturn += Aes.arrayToString(state);
            }
            ;
        }
        for (var i = 0; i < temp.length; i++) {
            {
                var index = i * 2;
                var j = parseInt(toReturn.substring(index, index + 2), 16);
                temp[i] = (j | 0);
            }
            ;
        }
        var s = String.fromCharCode.apply(null, temp);
        s = Aes.deletePadding(s);
        return s;
    };
    Aes.setBlockDecrypt = function (fullText, k) {
        var block = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([4, 4]);
        block = Aes.arrayToMatrixDecrypt(fullText, k);
        return block;
    };
    Aes.arrayToMatrixDecrypt = function (fullText, k) {
        var temp = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([4, 4]);
        for (var i = 0; i < 4; i++) {
            {
                for (var j = 0; j < 4; j++) {
                    {
                        temp[j][i] = fullText[k++];
                    }
                    ;
                }
            }
            ;
        }
        return temp;
    };
    Aes.arrayToString = function (byteMatrix) {
        var ret = "";
        var temp = "";
        for (var i = 0; i < 4; i++) {
            {
                for (var j = 0; j < 4; j++) {
                    {
                        if ((byteMatrix[j][i]).toString(16).length === 2) {
                            ret = ret + (byteMatrix[j][i]).toString(16);
                        }
                        else if ((byteMatrix[j][i]).toString(16).length === 1) {
                            ret = ret + "0" + (byteMatrix[j][i]).toString(16);
                        }
                        else {
                            temp = (byteMatrix[j][i]).toString(16);
                            ret = ret + temp.substring(temp.length - 2, temp.length);
                        }
                    }
                    ;
                }
            }
            ;
        }
        return ret;
    };
     Aes.generateKeys = function () {
        Aes.keys = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([4, 44]);
        var temp = [0, 0, 0, 0];
        Aes.initRound();
        for (var j = 4; j < 44; j++) {
            {
                if ((j) % 4 === 0) {
                    temp = Aes.rotWord(j);
                    Aes.subByte(j, temp);
                }
                for (var i = 0; i < 4; i++) {
                    {
                        if ((j) % 4 !== 0) {
                            Aes.keys[i][j] = (Aes.keys[i][j - 4] ^ Aes.keys[i][j - 1]);
                        }
                        else {
                            Aes.keys[i][j] = ((temp[i] ^ Aes.Rcon_$LI$()[i][((j / 4 | 0)) - 1]) ^ Aes.keys[i][j - 4]);
                        }
                    }
                    ;
                }
            }
            ;
        }
    };
    /*private*/ Aes.initRound = function () {
        var count = 0;
        for (var i = 0; i < 4; i++) {
            {
                for (var j = 0; j < 4; j++) {
                    {
                        Aes.keys[j][i] = (Aes.key.charAt(count)).charCodeAt(0);
                        count++;
                    }
                    ;
                }
            }
            ;
        }
    };
    /*private*/ Aes.rotWord = function (j) {
        var temp = [0, 0, 0, 0];
        j--;
        temp[3] = Aes.keys[0][j];
        for (var i = 0; i < 3; i++) {
            {
                temp[i] = Aes.keys[i + 1][j];
            }
            ;
        }
        return temp;
    };
    /*private*/ Aes.subByte = function (j, temp) {
        var r = 0;
        var c = 0;
        for (var i = 0; i < 4; i++) {
            {
                var s = (temp[i]).toString(16);
                if (s.length < 2) {
                    r = 0;
                    c = Aes.fixHex(s.charAt(0));
                }
                else {
                    c = Aes.fixHex(s.charAt(s.length - 1));
                    r = Aes.fixHex(s.charAt(s.length - 2));
                }
                temp[i] = Aes.sBox_$LI$()[r][c];
            }
            ;
        }
    };
    Aes.shiftRows = function (state) {
        var temp = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([4, 4]);
        temp[0] = state[0];
        for (var i = 1; i < state.length; i++) {
            {
                for (var j = 0; j < state[i].length; j++) {
                    {
                        temp[i][j] = state[i][(j + i) % 4];
                    }
                    ;
                }
            }
            ;
        }
        return temp;
    };
    Aes.addRoundKey = function (bytematrix, keymatrix, round) {
        for (var i = round * 4; i < (round * 4) + 4; i++) {
            {
                for (var j = 0; j < 4; j++) {
                    {
                        bytematrix[j][i % 4] ^= Aes.keys[j][i];
                    }
                    ;
                }
            }
            ;
        }
    };
    Aes.fixHex = function (value) {
        var a = 0;
        switch ((value).charCodeAt(0)) {
            case 48 /* '0' */:
                a = 0;
                break;
            case 49 /* '1' */:
                a = 1;
                break;
            case 50 /* '2' */:
                a = 2;
                break;
            case 51 /* '3' */:
                a = 3;
                break;
            case 52 /* '4' */:
                a = 4;
                break;
            case 53 /* '5' */:
                a = 5;
                break;
            case 54 /* '6' */:
                a = 6;
                break;
            case 55 /* '7' */:
                a = 7;
                break;
            case 56 /* '8' */:
                a = 8;
                break;
            case 57 /* '9' */:
                a = 9;
                break;
            case 65 /* 'A' */:
                a = 10;
                break;
            case 66 /* 'B' */:
                a = 11;
                break;
            case 67 /* 'C' */:
                a = 12;
                break;
            case 68 /* 'D' */:
                a = 13;
                break;
            case 69 /* 'E' */:
                a = 14;
                break;
            case 70 /* 'F' */:
                a = 15;
                break;
            case 97 /* 'a' */:
                a = 10;
                break;
            case 98 /* 'b' */:
                a = 11;
                break;
            case 99 /* 'c' */:
                a = 12;
                break;
            case 100 /* 'd' */:
                a = 13;
                break;
            case 101 /* 'e' */:
                a = 14;
                break;
            case 102 /* 'f' */:
                a = 15;
                break;
        }
        return a;
    };
    Aes.mixColumns = function (byteMatrix) {
        var stateNew = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([byteMatrix.length, byteMatrix[0].length]);
        for (var i = 0; i < 4; i++) {
            {
                stateNew[0][i] = Aes.xor4Bytes(Aes.multiply$int$int(byteMatrix[0][i], 2), Aes.multiply$int$int(byteMatrix[1][i], 3), byteMatrix[2][i], byteMatrix[3][i]);
                stateNew[1][i] = Aes.xor4Bytes(byteMatrix[0][i], Aes.multiply$int$int(byteMatrix[1][i], 2), Aes.multiply$int$int(byteMatrix[2][i], 3), byteMatrix[3][i]);
                stateNew[2][i] = Aes.xor4Bytes(byteMatrix[0][i], byteMatrix[1][i], Aes.multiply$int$int(byteMatrix[2][i], 2), Aes.multiply$int$int(byteMatrix[3][i], 3));
                stateNew[3][i] = Aes.xor4Bytes(Aes.multiply$int$int(byteMatrix[0][i], 3), byteMatrix[1][i], byteMatrix[2][i], Aes.multiply$int$int(byteMatrix[3][i], 2));
            }
            ;
        }
        return stateNew;
    };
    Aes.multiply$int$int = function (v1, v2) {
        return (Aes.multiply$byte$byte((v1 | 0), (v2 | 0)) | 0);
    };
    Aes.convertToBinary = function (no, no2) {
        var container = [0, 0, 0, 0, 0, 0, 0, 0];
        var s = no.toString(2);
        var test = { str: s, toString: function () { return this.str; } };
        while ((test.str.length < 4)) {
            {
                /* insert */ (function (sb, index, c) { sb.str = sb.str.substr(0, index) + c + sb.str.substr(index); return sb; })(test, 0, '0');
            }
        }
        ;
        s = test.str;
        var s2 = no2.toString(2);
        test = { str: s2, toString: function () { return this.str; } };
        while ((test.str.length < 4)) {
            {
                /* insert */ (function (sb, index, c) { sb.str = sb.str.substr(0, index) + c + sb.str.substr(index); return sb; })(test, 0, '0');
            }
        }
        ;
        s2 = test.str;
        var fin = s.concat(s2);
        for (var i = 0; i < 8; i++) {
            {
                container[i] = (function (c) { return c.charCodeAt == null ? c : c.charCodeAt(0); })(fin.charAt(i)) - 48;
            }
            ;
        }
        return container;
    };
    Aes.multiply$byte$byte = function (v1, v2) {
        if (v1 < 0) {
            v1 += 256;
        }
        var num = v1.toString(2);
        var tem = v1;
        var sb = { str: num, toString: function () { return this.str; } };
        var b = 27;
        while ((sb.str.length < 8)) {
            {
                /* insert */ (function (sb, index, c) { sb.str = sb.str.substr(0, index) + c + sb.str.substr(index); return sb; })(sb, 0, '0');
            }
        }
        ;
        num = sb.str;
        var mulb = false;
        if ((function (c) { return c.charCodeAt == null ? c : c.charCodeAt(0); })(num.charAt(num.length - 8)) == '1'.charCodeAt(0)) {
            mulb = true;
        }
        if (v2 === 2) {
            v1 = ((((v1 << 1) | 0)) | 0);
            if (mulb === true) {
                v1 = ((v1 ^ b) | 0);
            }
            return v1;
        }
        else if (v2 === 3) {
            v1 = ((((v1 << 1) | 0)) | 0);
            if (mulb === true) {
                v1 = ((v1 ^ b) | 0);
            }
            v1 = (((v1 | 0) ^ tem) | 0);
            return v1;
        }
        else {
            var bTemps = [0, 0, 0, 0, 0, 0, 0, 0];
            var bResult = 0;
            bTemps[0] = v1;
            for (var i = 1; i < bTemps.length; i++) {
                {
                    bTemps[i] = Aes.xtime(bTemps[i - 1]);
                }
                ;
            }
            for (var i = 0; i < bTemps.length; i++) {
                {
                    if (Aes.getBit(v2, i) !== 1) {
                        bTemps[i] = 0;
                    }
                    bResult ^= bTemps[i];
                }
                ;
            }
            return bResult;
        }
    };
    Aes.multiply = function (v1, v2) {
        if (((typeof v1 === 'number') || v1 === null) && ((typeof v2 === 'number') || v2 === null)) {
            return Aes.multiply$byte$byte(v1, v2);
        }
        else if (((typeof v1 === 'number') || v1 === null) && ((typeof v2 === 'number') || v2 === null)) {
            return Aes.multiply$int$int(v1, v2);
        }
        else
            throw new Error('invalid overload');
    };
    Aes.getBit = function (value, i) {
        var bMasks = [(1 | 0), (2 | 0), (4 | 0), (8 | 0), (16 | 0), (32 | 0), (64 | 0), (128 | 0)];
        var bBit = ((value & bMasks[i]) | 0);
        return ((((bBit >> i) | 0) & (1 | 0)) | 0);
    };
    Aes.xtime = function (value) {
        var iResult = 0;
        iResult = ((value & 255) | 0) * 2;
        return ((((iResult & 256) !== 0) ? iResult ^ 283 : iResult) | 0);
    };
    Aes.prototype.BinaryToDecimal = function (binaryNumber) {
        var decimal = 0;
        var p = 0;
        while ((true)) {
            {
                if (binaryNumber === 0) {
                    break;
                }
                else {
                    var temp = binaryNumber % 10;
                    decimal += temp * Math.pow(2, p);
                    binaryNumber = (binaryNumber / 10 | 0);
                    p++;
                }
            }
        }
        ;
        return decimal;
    };
    Aes.toDecimal = function (arr) {
        var toReturn = 0;
        var p = 0;
        for (var i = 7; i >= 0; i--) {
            {
                toReturn = ((toReturn + arr[i] * Math.pow(2, p++)) | 0);
            }
            ;
        }
        return toReturn;
    };
    Aes.shiftArray$int_A = function (arr) {
        for (var i = 0; i < 7; i++) {
            {
                arr[i] = arr[i + 1];
            }
            ;
        }
        arr[7] = 0;
        return arr;
    };
    Aes.shiftArray = function (arr) {
        if (((arr != null && arr instanceof Array && (arr.length == 0 || arr[0] == null || (typeof arr[0] === 'number'))) || arr === null)) {
            return Aes.shiftArray$int_A(arr);
        }
        else if (((typeof arr === 'string') || arr === null)) {
            return Aes.shiftArray$java_lang_String(arr);
        }
        else
            throw new Error('invalid overload');
    };
    Aes.shiftArray$java_lang_String = function (arr) {
        var toReturn = [null, null, null, null, null, null, null, null];
        for (var i = 0; i < 7; i++) {
            {
                toReturn[i] = ('' + (arr.charAt(i)));
            }
            ;
        }
        toReturn[7] = "0";
        return toReturn;
    };
    Aes.xor = function (arr, second) {
        for (var i = 0; i < 8; i++) {
            {
                arr[i] ^= second[i];
            }
            ;
        }
        return arr;
    };
    Aes.xor4Bytes = function (b1, b2, b3, b4) {
        var bResult = 0;
        bResult = ((bResult ^ b1) | 0);
        bResult = ((bResult ^ b2) | 0);
        bResult = ((bResult ^ b3) | 0);
        bResult = ((bResult ^ b4) | 0);
        return bResult;
    };
    Aes.subBytes = function (state) {
        for (var i = 0; i < state.length; i++) {
            {
                for (var j = 0; j < state[i].length; j++) {
                    {
                        state[i][j] = Aes.sboxTransform(state[i][j]);
                    }
                    ;
                }
            }
            ;
        }
        return state;
    };
    Aes.sboxTransform = function (value) {
        var bUpper = 0;
        var bLower = 0;
        bUpper = ((((value >> 4) | 0) & 15) | 0);
        bLower = ((value & 15) | 0);
        return (Aes.sBox_$LI$()[bUpper][bLower] | 0);
    };
    Aes.invShiftRows = function (state) {
        var temp = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([4, 4]);
        temp[0] = state[0];
        for (var i = 1; i < state.length; i++) {
            {
                for (var j = 0; j < state[i].length; j++) {
                    {
                        temp[i][j] = state[i][(j - i + 4) % 4];
                    }
                    ;
                }
            }
            ;
        }
        return temp;
    };
    Aes.invSubBytes = function (state) {
        for (var i = 0; i < state.length; i++) {
            {
                for (var j = 0; j < state[i].length; j++) {
                    {
                        state[i][j] = Aes.invSboxTransform(state[i][j]);
                    }
                    ;
                }
            }
            ;
        }
        return state;
    };
    Aes.invSboxTransform = function (value) {
        var bUpper = 0;
        var bLower = 0;
        bUpper = ((((value >> 4) | 0) & 15) | 0);
        bLower = ((value & 15) | 0);
        return Aes.sBoxInv_$LI$()[bUpper][bLower];
    };
    Aes.invMixColumns = function (state) {
        var stateNew = (function (dims) { var allocate = function (dims) { if (dims.length == 0) {
            return 0;
        }
        else {
            var array = [];
            for (var i = 0; i < dims[0]; i++) {
                array.push(allocate(dims.slice(1)));
            }
            return array;
        } }; return allocate(dims); })([state.length, state[0].length]);
        for (var c = 0; c < 4; c++) {
            {
                stateNew[0][c] = Aes.xor4Bytes(Aes.multiply$int$int(state[0][c], 14), Aes.multiply$int$int(state[1][c], 11), Aes.multiply$int$int(state[2][c], 13), Aes.multiply$int$int(state[3][c], 9));
                stateNew[1][c] = Aes.xor4Bytes(Aes.multiply$int$int(state[0][c], 9), Aes.multiply$int$int(state[1][c], 14), Aes.multiply$int$int(state[2][c], 11), Aes.multiply$int$int(state[3][c], 13));
                stateNew[2][c] = Aes.xor4Bytes(Aes.multiply$int$int(state[0][c], 13), Aes.multiply$int$int(state[1][c], 9), Aes.multiply$int$int(state[2][c], 14), Aes.multiply$int$int(state[3][c], 11));
                stateNew[3][c] = Aes.xor4Bytes(Aes.multiply$int$int(state[0][c], 11), Aes.multiply$int$int(state[1][c], 13), Aes.multiply$int$int(state[2][c], 9), Aes.multiply$int$int(state[3][c], 14));
            }
            ;
        }
        return stateNew;
    };
    Aes.padding = function (plainText) {
        var i = 0;
        while (((plainText.length % 16) !== 0)) {
            {
                if (i === 0) {
                    plainText += "@";
                    i++;
                }
                else {
                    plainText += "0";
                }
            }
        }
        ;
        return plainText;
    };
    Aes.deletePadding = function (input) {
        var hasPadding = false;
        var a = input.length - 1;
        if ((function (c) { return c.charCodeAt == null ? c : c.charCodeAt(0); })(input.charAt(a)) == '0'.charCodeAt(0) || (function (c) { return c.charCodeAt == null ? c : c.charCodeAt(0); })(input.charAt(a)) == '1'.charCodeAt(0)) {
            hasPadding = true;
        }
        if (!hasPadding) {
            return input;
        }
        for (var i = input.length - 1; i > input.length - 15; i--) {
            {
                if ((function (c) { return c.charCodeAt == null ? c : c.charCodeAt(0); })(input.charAt(i)) == '0'.charCodeAt(0)) {
                    input = input.substring(0, input.length-1);
                }
                else if ((function (c) { return c.charCodeAt == null ? c : c.charCodeAt(0); })(input.charAt(i)) == '@'.charCodeAt(0)) {
                    input = input.substring(0, input.length-1);
                    return input;
                }
            }
            ;
        }
        return input;
    };
    return Aes;
}());
Aes.keys = null;
Aes.k = 0;
Aes.key = "";
Aes["__class"] = "Aes";
Aes.sBoxInv_$LI$();
Aes.sBox_$LI$();
Aes.Rcon_$LI$();
module.exports = Aes;