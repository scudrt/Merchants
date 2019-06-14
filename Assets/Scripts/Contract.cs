using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract {
    /************ Interface ************/
    public float offeredFund { private set { } get {
            return Mathf.Max(_offeredFund, 0f);
        } }
    public float requiredFund { private set { } get {
            return Mathf.Max(-_offeredFund, 0f);
        } }
    public List<int> offeredTalents { private set; get; }
    public List<int> requiredTalents { private set; get; }
    public List<int> offeredBlocks { private set; get; }
    public List<int> requiredBlocks { private set; get; }
    /****************trading parties****************/
    public int _offererId, _targetId; //id of companies
    /****************trading content****************/
    private List<int> _offeredTalentList, _requiredTalentList;
    private List<int> _offeredBlockList, _requiredBlockList;
    private float _offeredFund;
    /****************other private arguments****************/
    private float timeout = 10f; //time delay of waiting for response
    private bool _isConfirmed; //is contract confirmed
    private bool _isTargetAgreed; //whether the target company agree with this contract

    private Contract() { }
    public Contract(Company offerer, Company target = null) {
        //initialise the contract info
        _offeredFund = 0f;
        _isConfirmed = false;
        _isTargetAgreed = false;

        _offererId = offerer.id;
        _targetId = target == null ? -1 : target.id;

        _offeredTalentList = new List<int>();
        _requiredTalentList = new List<int>();
        _offeredBlockList = new List<int>();
        _requiredBlockList = new List<int>();

        offeredTalents = _offeredTalentList;
        requiredTalents = _requiredTalentList;
        offeredBlocks = _offeredBlockList;
        requiredBlocks = _requiredBlockList;
    }
    /**************** private functions ****************/
    private void waitForTargetResponse() { //TO BE DONE
        //return true if the target company responded this contract
        //_isTargetAgreed = something
    }

    private bool deal() { //TO BE DONE
        //trade the resource, remember to notify target
        //return false if one of the parties doesn't have enough resource
        if (_isTargetAgreed) {
            //swap resource
            ;
            return true;
        } else {
            return false;
        }
    }

    /**************** interface functions ****************/
    public bool confirm() {
        //commit the contract to the target company
        //return false if failed
        if (_offererId == -1 || _targetId == -1) {
            return false;
        }
        _isConfirmed = true;
        Client client = GameObject.FindObjectOfType<Client>();
        ContractInfo info = new ContractInfo(this);
        client.SendServer((NetMsg)info);
        //发送消息

        return true;
    }

    public bool cancel() {
        //clear all references, helpful for garbage collection
        
        _offererId = -1;
        _targetId = -1;

        _offeredBlockList.Clear();
        _offeredTalentList.Clear();
        _requiredBlockList.Clear();
        _requiredTalentList.Clear();
        return true;
    }

    public void agree() {
        //can only be called by target
        
        if (this._targetId != City.currentCompany.id)
        {
            return;
        }
        else
        {
            this._isTargetAgreed = true;
            Client client = GameObject.FindObjectOfType<Client>();
            ContractInfo info = new ContractInfo(this);
            client.SendServer((NetMsg)info);
            Debug.Log("同意合同");
            //发送同意的消息
        }
    }

    public void refuse() {
        //can only be called by target
        if (this._targetId != City.currentCompany.id)
        {
            return;
        }
        else
        {
            this._isTargetAgreed = false;
            Client client = GameObject.FindObjectOfType<Client>();
            ContractInfo info = new ContractInfo(this);
            client.SendServer((NetMsg)info);
            Debug.Log("拒绝合同");
            //发送拒绝的消息
        }
    }

    public void setTarget(Company target) { //set target company
        if (target.id == _offererId) {
            return;
        }
        _targetId = target.id;
    }

    public void setOfferedFund(float fund) { //negative means require money
        _offeredFund = fund;
    }

    public void offerFundBy(float deltaFund) { //negative means require money
        _offeredFund += deltaFund;
    }

    public void addTalent(Talent talent) { //offer or require talent in this contract
        if (talent.companyBelong.id == _offererId) {
            if (_offeredTalentList.Contains(talent.id)) {
                //already exists
                return;
            }
            _offeredTalentList.Add(talent.id);
        }else if (talent.companyBelong.id == _targetId) {
            if (_requiredTalentList.Contains(talent.id)) {
                return;
            }
            _requiredTalentList.Add(talent.id);
        } else {
            //wrong talent, error
            Debug.Log("Contract: wrong talent input.");
        }
    }

    public void addBlock(Block block) { //offer or require block in this contract
        if (block.companyBelong.id == _offererId) {
            if (_offeredBlockList.Contains(block.id)) {
                return;
            }
            _offeredBlockList.Add(block.id);
        } else if (block.companyBelong.id == _targetId) {
            if (_requiredBlockList.Contains(block.id)) {
                return;
            }
            _requiredBlockList.Add(block.id);
        } else {
            //wrong block, error
            Debug.Log("Contract: wrong block input.");
        }
    }

    public void removeTalent(Talent talent) { //remove talent from contract
        if (_offeredTalentList.Contains(talent.id)) {
            _offeredTalentList.Remove(talent.id);
        }else if (_requiredTalentList.Contains(talent.id)) {
            _requiredTalentList.Remove(talent.id);
        } else {
            //invalid talent
            Debug.Log("Contract: try to remove talent that doesn't exist");
        }
    }

    public void removeBlock(Block block) { //remove block from contract
        if (_offeredBlockList.Contains(block.id)) {
            _offeredBlockList.Remove(block.id);
        } else if (_requiredBlockList.Contains(block.id)) {
            _requiredBlockList.Remove(block.id);
        } else {
            //invalid block
            Debug.Log("Contract: try to remove block that doesn't exist");
        }
    }
}
