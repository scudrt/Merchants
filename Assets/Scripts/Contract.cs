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
    public List<Talent> offeredTalents { private set; get; }
    public List<Talent> requiredTalents { private set; get; }
    public List<Block> offeredBlocks { private set; get; }
    public List<Block> requiredBlocks { private set; get; }
    /****************trading parties****************/
    private Company _offerer, _target;
    /****************trading content****************/
    private List<Talent> _offeredTalentList, _requiredTalentList;
    private List<Block> _offeredBlockList, _requiredBlockList;
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

        _offerer = offerer;
        _target = target;

        _offeredTalentList = new List<Talent>();
        _requiredTalentList = new List<Talent>();
        _offeredBlockList = new List<Block>();
        _requiredBlockList = new List<Block>();

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
        if (_offerer == null || _target == null) {
            return false;
        }
        _isConfirmed = true;
        waitForTargetResponse();
        return deal();
    }

    public bool cancel() {
        //clear all references, helpful for garbage collection
        _offerer = null;
        _target = null;

        _offeredBlockList.Clear();
        _offeredTalentList.Clear();
        _requiredBlockList.Clear();
        _requiredTalentList.Clear();
        return true;
    }

    public void setTarget(Company target) { //set target company
        if (target == _offerer) {
            return;
        }
        _target = target;
    }

    public void setOfferedFund(float fund) { //negative means require money
        _offeredFund = fund;
    }

    public void offerFundBy(float deltaFund) { //negative means require money
        _offeredFund += deltaFund;
    }

    public void addTalent(Talent talent) { //offer or require talent in this contract
        if (talent.companyBelong == _offerer) {
            if (_offeredTalentList.Contains(talent)) {
                //already exists
                return;
            }
            _offeredTalentList.Add(talent);
        }else if (talent.companyBelong == _target) {
            if (_requiredTalentList.Contains(talent)) {
                return;
            }
            _requiredTalentList.Add(talent);
        } else {
            //wrong talent, error
            Debug.Log("Contract: wrong talent input.");
        }
    }

    public void addBlock(Block block) { //offer or require block in this contract
        if (block.companyBelong == _offerer) {
            if (_offeredBlockList.Contains(block)) {
                return;
            }
            _offeredBlockList.Add(block);
        } else if (block.companyBelong == _target) {
            if (_requiredBlockList.Contains(block)) {
                return;
            }
            _requiredBlockList.Add(block);
        } else {
            //wrong block, error
            Debug.Log("Contract: wrong block input.");
        }
    }

    public void removeTalent(Talent talent) { //remove talent from contract
        if (_offeredTalentList.Contains(talent)) {
            _offeredTalentList.Remove(talent);
        }else if (_requiredTalentList.Contains(talent)) {
            _requiredTalentList.Remove(talent);
        } else {
            //invalid talent
            Debug.Log("Contract: try to remove talent that doesn't exist");
        }
    }

    public void removeBlock(Block block) { //remove block from contract
        if (_offeredBlockList.Contains(block)) {
            _offeredBlockList.Remove(block);
        } else if (_requiredBlockList.Contains(block)) {
            _requiredBlockList.Remove(block);
        } else {
            //invalid block
            Debug.Log("Contract: try to remove block that doesn't exist");
        }
    }
}
