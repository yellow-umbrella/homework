// SPDX-License-Identifier: GPL-3.0

pragma solidity ^0.8.0;

contract TreasureBox {
    address public owner;
    bytes32 public secret;
    bool public claimed;

    mapping(bytes32 => address) public commitments;

    constructor(bytes32 _secret) payable {
        require(msg.value > 0, "You forgot the reward");
        owner = msg.sender;
        secret = _secret;
        claimed = false;
    }

    function commit(bytes32 hash) public {
        require(!claimed, "Already claimed");
        require(commitments[hash] == address(0), "Already used");
        commitments[hash] = msg.sender;
    }

    function claim(string memory message, bytes32 salt) public {
        require(!claimed, "Already claimed");
        bytes32 hash = keccak256(abi.encodePacked(message, salt));
        require(commitments[hash] == msg.sender, "Did not commit");
        require(keccak256(bytes(message)) == secret, "Wrong answer");
        claimed = true;
        payable(msg.sender).transfer(address(this).balance);
    }

    function reinit(bytes32 _secret) public {
        require(msg.sender == owner, "Permission denied");
        require(address(this).balance > 0, "You forgot the reward");
        secret = _secret;
        claimed = false;
    }

    receive() external payable {}
}